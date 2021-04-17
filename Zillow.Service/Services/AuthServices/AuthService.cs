using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Zillow.Core.Dto.AuthDto;
using Zillow.Core.Exceptions;
using Zillow.Core.Options;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserDbEntity> _userManager;
        private readonly SignInManager<UserDbEntity> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly MyJwtBearerOptions _jwtOptions;
        private readonly EntityNotFoundException _notFoundException;

        public AuthService(UserManager<UserDbEntity> userManager, SignInManager<UserDbEntity> signInManager,
            IOptions<MyJwtBearerOptions> options, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = options.Value;
            _dbContext = dbContext;
            _notFoundException = new EntityNotFoundException("User");
        }

        public TokenViewModel GenerationAccessToken(UserDbEntity user)
        {
            // var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            /*  if (roles.Any())
              {
                  claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
              }
             */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddHours(1);

            var accessToken = new JwtSecurityToken(issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Site,
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return new TokenViewModel()
            {
                ExpireAt = expires,
                Token = new JwtSecurityTokenHandler().WriteToken(accessToken)
            };
        }

        public async Task<LoginResponseViewModel> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            var result = await _signInManager
                .PasswordSignInAsync(dto.Email, dto.Password, false, false);
            
            if (user == null||!result.Succeeded)
                throw new LoginFailureException();
            
            var accessToken = GenerationAccessToken(user);

            if (string.IsNullOrEmpty(user.RefreshToken))
            {
                user.RefreshToken = GenerateRefreshToken();
                await _userManager.UpdateAsync(user);
            }

            var refreshToken = user.RefreshToken;

            return new LoginResponseViewModel()
            {
                UserId = user.Id,
                TokenViewModel = accessToken,
                RefreshToken = refreshToken
            };
        }

        public async Task<string> Logout(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null) throw _notFoundException;
            
            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return userId;
        }


        public LoginResponseViewModel RefreshToken(string refreshToken)
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.RefreshToken == refreshToken);

            if (user == null)
                throw _notFoundException;

            return new LoginResponseViewModel()
            {
                TokenViewModel = GenerationAccessToken(user),
                UserId = user.Id,
                RefreshToken = refreshToken
            };
        }

        public async Task<string> RegisterFcmToken(string userId, string userFcmToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw _notFoundException;
            
            user.FcmToken = userFcmToken;

            await _userManager.UpdateAsync(user);

            return userId;
        }


        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}