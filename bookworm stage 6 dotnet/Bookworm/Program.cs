using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Bookworm.Repository;
using Bookworm.Repositories;
using Bookworm.Services;
using Bookworm.Services.Impl;
using Bookworm.Service;
using Bookworm.ServicesImpl;
using Bookworm.OrderService;
using Bookworm.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configure MySQL EF Core with Pomelo
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookwormDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --- Add this section to load and register your email configuration ---
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

// --- Register the new services for PDF and Emailing ---
builder.Services.AddScoped<IPdfInvoiceService, PdfInvoiceServiceImpl>();
builder.Services.AddScoped<IEmailSender, EmailSender>(); // Register the email sender

// Register repositories
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartDetailRepository, CartDetailRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
builder.Services.AddScoped<IUserLibraryRepository, UserLibraryRepository>();
builder.Services.AddScoped<IRentalLedgerRepository, RentalLedgerRepository>();
builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
builder.Services.AddScoped<IProductBeneficiaryRepository, ProductBeneficiaryRepository>();
builder.Services.AddScoped<IBeneficiaryMasterRepository, BeneficiaryMasterRepository>();
builder.Services.AddScoped<IRoyaltyCalculationRepository, RoyaltyCalculationRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
// Register services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserLibraryService, UserLibraryService>();
builder.Services.AddScoped<IRoyaltyCalculationService, RoyaltyCalculationServiceImpl>();
builder.Services.AddScoped<IProductBeneficiaryService, ProductBeneficiaryService>();
builder.Services.AddScoped<IBeneficiaryMasterService, BeneficiaryMasterService>();
builder.Services.Configure<RazorpaySettings>(builder.Configuration.GetSection("Razorpay"));

// Program.cs
builder.Services.AddScoped<IOrderService, OrderService>();

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Configure Authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("ROLE_ADMIN"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("ROLE_USER"));
});

// Configure CORS to allow React app access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();

    });
});

var app = builder.Build();

// Middleware pipeline
app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// Seed database roles asynchronously
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<BookwormDbContext>();
    await SeedDatabaseAsync(dbContext);
}

app.Run();

static async Task SeedDatabaseAsync(BookwormDbContext dbContext)
{
    if (!await dbContext.Roles.AnyAsync(r => r.Name == "ROLE_USER"))
    {
        await dbContext.Roles.AddAsync(new Bookworm.Models.Role { Name = "ROLE_USER" });
        await dbContext.SaveChangesAsync();
    }
    if (!await dbContext.Roles.AnyAsync(r => r.Name == "ROLE_ADMIN"))
    {
        await dbContext.Roles.AddAsync(new Bookworm.Models.Role { Name = "ROLE_ADMIN" });
        await dbContext.SaveChangesAsync();
    }
}
