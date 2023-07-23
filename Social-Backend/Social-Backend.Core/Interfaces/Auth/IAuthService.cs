using Social_Backend.Application.Common.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Core.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(LoginRequest request);

        Task<AuthResponse> RefreshToken(RefreshTokenRequest request);

        Task RevokeToken(string userId);

        Task RevokeAllToken();

        Task<bool> Register(RegisterRequest request);
    }
}