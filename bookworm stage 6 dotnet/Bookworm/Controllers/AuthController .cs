using Microsoft.AspNetCore.Mvc;
using Bookworm.DTO;
using Bookworm.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Bookworm.Services;
using System.Security.Cryptography;
using System.Text;
using Bookworm.Repository;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly BookwormDbContext _dbContext;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _config;

        public AuthController(BookwormDbContext dbContext, IJwtService jwtService, IConfiguration config)
        {
            _dbContext = dbContext;
            _jwtService = jwtService;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequest loginRequest)
        {
            // Step 1: Find the user by email
            var customer = await _dbContext.Customers
                .Include(c => c.CustomerRoles)
                .ThenInclude(cr => cr.Role)
                .FirstOrDefaultAsync(c => c.Email == loginRequest.Email);

            if (customer == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Step 2: Verify the password
            if (!VerifyPasswordHash(loginRequest.Password, customer.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Step 3: Generate the JWT token

            string token = _jwtService.GenerateToken(customer);

            // Step 4: Create the response with token, ID, and name
            var jwtResponse = new JwtResponse(token, customer.Id, customer.Name);
            return Ok(jwtResponse);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            // Step 1: Check if the email is already in use
            if (await _dbContext.Customers.AnyAsync(c => c.Email == registerRequest.Email))
            {
                return BadRequest(new { message = "Error: Email is already in use!" });
            }

            // Step 2: Find the default 'ROLE_USER'
            var userRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "ROLE_USER");
            if (userRole == null)
            {
                return StatusCode(500, new { message = "Error: Role 'ROLE_USER' not found." });
            }

            // Step 3: Hash the password and create the new customer
            string passwordHash = CreatePasswordHash(registerRequest.Password);

            var newCustomer = new Customer
            {
                Name = registerRequest.Name,
                Email = registerRequest.Email,
                PasswordHash = passwordHash,
                CustomerRoles = new List<CustomerRole>
                {
                    new CustomerRole { Role = userRole }
                }
            };

            // Step 4: Save the new customer to the database
            await _dbContext.Customers.AddAsync(newCustomer);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "User registered successfully!" });
        }

        // =======================================================================
        // Password Hashing and Verification
        // In a real application, this logic might be in a separate service.
        // For this example, we'll keep it here for simplicity.
        // =======================================================================
        private string CreatePasswordHash(string password)
        {
            // Simple password hashing using SHA256 for demonstration.
            // A more robust library like `BCrypt.Net` is recommended for production.
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // Simple password verification for demonstration.
            // A more robust library like `BCrypt.Net` is recommended for production.
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var passwordHash = Convert.ToBase64String(bytes);
                return passwordHash == storedHash;
            }
        }
    }
}