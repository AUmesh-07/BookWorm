using System.Security.Claims;
using System.Collections.Generic;
using Bookworm.Models;

namespace Bookworm.Services
{
    public interface IJwtService
    {
        string GenerateToken(Customer customer);
    }
}