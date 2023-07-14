using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Social_Backend.Application.Common.Constants;
using Social_Backend.Application.Common.Exceptions;
using Social_Backend.Application.Common.Models.Auth;
using Social_Backend.Core.Entities;
using Social_Backend.Core.Interfaces;
using Social_Backend.Core.Interfaces.Auth;
using Social_Backend.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Backend.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUploadService _uploadService;

        public AuthService(SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenService tokenService, IUploadService uploadService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _uploadService = uploadService;
        }

        public async Task<AuthResponse> Authenticate(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                    throw new NotFoundException("Username/password is incorrect");
                var res = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);
                if (res.IsLockedOut)
                {
                    throw new ForbiddenAccessException("Your account has been lockout, unlock in " + user.LockoutEnd);
                }
                if (!res.Succeeded)
                    throw new NotFoundException("Username/password is incorrect");
                if (user.Status == USER_STATUS.IN_ACTIVE)
                    throw new ForbiddenAccessException("Your account has been banned");
                //if (!user.EmailConfirmed)
                //    throw new ForbiddenAccessException("Your account hasn't been confirmed");

                string accessToken = await _tokenService.CreateJWT(user.Id);
                string refreshToken = _tokenService.CreateRefreshToken();
                DateTime refreshTokenExpiredTime = DateTime.Now.AddDays(7);
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiredTime = refreshTokenExpiredTime;
                var isSuccess = await _userManager.UpdateAsync(user);
                if (!isSuccess.Succeeded)
                    throw new Exception("Cannot login, please contact administrator");
                return new AuthResponse { AccessToken = accessToken, RefreshToken = refreshToken };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<AuthResponse> RefreshToken(AuthResponse request)
        {
            var userPrincipal = _tokenService.ValidateExpiredJWT(request.AccessToken);
            if (userPrincipal is null)
            {
                throw new UnauthorizedException("Invalid access token");
            }
            var userName = userPrincipal.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiredTime <= DateTime.Now)
            {
                throw new UnauthorizedException("Invalid access token or refresh token");
            }
            var newAccessToken = await _tokenService.CreateJWT(user.Id);
            var newRefreshToken = _tokenService.CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthResponse { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            try
            {
                var user = new AppUser()
                {
                    DateOfBirth = request.Dob,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserName = request.UserName,
                    PhoneNumber = request.PhoneNumber,
                    Gender = request.Gender,
                    Status = USER_STATUS.ACTIVE,
                };
                if (request.Avatar != null)
                {
                    user.Avatar = await _uploadService.UploadFile(request.Avatar);
                }
                else
                {
                    user.Avatar = "default-user.png";
                }
                var res = await _userManager.CreateAsync(user, request.Password);

                if (res.Succeeded)
                {
                    List<string> roles = new()
                {
                   USER_ROLE.CUSTOMER_ROLE
                };
                    await _userManager.AddToRolesAsync(user, roles);
                    //if (!string.IsNullOrEmpty(request.LoginProvider))
                    //{
                    //    await _userManager.AddLoginAsync(user, new UserLoginInfo(request.LoginProvider, request.ProviderKey, request.LoginProvider));
                    //}
                    //if (!string.IsNullOrEmpty(request.Host))
                    //{
                    //    bool isSend = await SendConfirmToken(user, request.Host);
                    //    if (!isSend)
                    //    {
                    //        throw new Exception("Cannot send mail");
                    //    }
                    //}
                    return true;
                }

                string error = "";
                res.Errors.ToList().ForEach(x => error += (x.Description + "/n"));
                throw new Exception(error);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RevokeAllToken()
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiredTime = null;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task RevokeToken(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
            user.RefreshToken = null;
            user.RefreshTokenExpiredTime = null;
            await _userManager.UpdateAsync(user);
        }
    }
}