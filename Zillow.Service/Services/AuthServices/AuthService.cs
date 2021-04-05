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
using Zillow.Core.Options;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.AuthServices
{
    public class AuthService : IAuthService
    {

        private readonly IMapper _mapper;
        private readonly UserManager<UserDbEntity> _userManager;
        private readonly SignInManager<UserDbEntity> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        private readonly MyJwtBearerOptions _jwtOptions;

        public AuthService(IMapper mapper, UserManager<UserDbEntity> userManager, SignInManager<UserDbEntity> signInManager, IOptions<MyJwtBearerOptions> options, ApplicationDbContext dbContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtOptions = options.Value;
            _dbContext = dbContext;
        }

        public async Task<TokenViewModel> GenerationAccessToken(UserDbEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

          /*  if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, string.Join(",", roles)));
            }
           */

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddHours(1);

            var accessToken = new JwtSecurityToken(issuer:_jwtOptions.Issuer,
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

            // Throw User Not Found Exception

            if (user == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false,false);

            // Throw Login Faliure Exception 

            if (!result.Succeeded)
                return null;


            var accessToken = await GenerationAccessToken(user);

            if (string.IsNullOrEmpty(user.RefreshToken))
            {
                user.RefreshToken = GenerateRefreshToken();
                await _userManager.UpdateAsync(user);
            }

            var refreshToken = user.RefreshToken;

            await _userManager.UpdateAsync(user);

            return new LoginResponseViewModel()
            {
                UserId=user.Id,
                TokenViewModel = accessToken,
                RefreshToken = refreshToken
            };

        }

        public async Task<string> Logout(string token)
        {
            var principal = new JwtSecurityTokenHandler()
         .ValidateToken(token,
             new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidIssuer = _jwtOptions.Issuer,
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
                 ValidAudience = _jwtOptions.Site,
                 ValidateAudience = true,
                 ValidateLifetime = false,
                 ClockSkew = TimeSpan.FromMinutes(1)
             },
             out var validatedToken);

            var userId = principal.Claims.SingleOrDefault(x=> x.Type == JwtRegisteredClaimNames.NameId).Value;

            var user = await _userManager.FindByIdAsync(userId);

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return userId;
        }

        

        public async Task<LoginResponseViewModel> RefreshToken(string refreshToken)
        {
            var user =  _dbContext.Users.SingleOrDefault(x => x.RefreshToken == refreshToken);

            return new LoginResponseViewModel()
            {
                TokenViewModel = await GenerationAccessToken(user),
                UserId = user.Id,
                RefreshToken = refreshToken
            };
        }

        public async Task<string> RegisterFcmToken(string userId, string userFcmToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.FcmToken = userFcmToken;

            await _userManager.UpdateAsync(user);

            return userId;
        }


        public static string GenerateRefreshToken ()
        {
            var randomNumber = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

    }
}
