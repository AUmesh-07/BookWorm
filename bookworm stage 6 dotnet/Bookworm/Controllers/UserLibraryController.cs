    using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookworm.Services;
using Bookworm.ResponseDTO;

namespace Bookworm.Controllers
{
    [ApiController]
    [Route("api/user-collection")]
    public class UserLibraryController : ControllerBase
    {
        private readonly IUserLibraryService _userLibraryService;

        public UserLibraryController(IUserLibraryService userLibraryService)
        {
            _userLibraryService = userLibraryService;
        }

        /// <summary>
        /// Get a user's purchased book shelf ("MyShelf").
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>ShelfResponseDTO</returns>
        [HttpGet("{customerId}/shelf")]
        [Authorize(Roles = "ROLE_USER")]
        public async Task<ActionResult<ShelfResponseDTO>> GetMyShelf(int customerId)
        {
            // TODO: Add security check to confirm the logged-in user matches customerId if required

            var shelfResponse = await _userLibraryService.GetShelfForCustomerAsync(customerId);
            return Ok(shelfResponse);
        }

        /// <summary>
        /// Get a user's active rental library ("MyLibrary").
        /// </summary>
        /// <param name="customerId">Customer ID</param>
        /// <returns>LibraryResponseDTO</returns>
        [HttpGet("{customerId}/library")]
        [Authorize(Roles = "ROLE_USER")]
        public async Task<ActionResult<LibraryResponseDTO>> GetMyLibrary(int customerId)
        {
            // TODO: Add security check to confirm the logged-in user matches customerId if required

            var libraryResponse = await _userLibraryService.GetLibraryForCustomerAsync(customerId);
            return Ok(libraryResponse);
        }
    }
}
