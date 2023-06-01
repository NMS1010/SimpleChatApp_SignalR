using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateJWT(string userId);

        ClaimsPrincipal ValidateExpiredJWT(string token);

        string CreateRefreshToken();
    }
}