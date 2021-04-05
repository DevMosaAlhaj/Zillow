using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserDbEntity> _userManager;
        private readonly SignInManager<UserDbEntity> _signInManager;
        public UserService(UserManager<UserDbEntity> userManager,
            SignInManager<UserDbEntity> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public List<UserViewModel> GetAll()
        {
            var users = _userManager.Users.Select(x => new UserViewModel()
            {
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.Email
            }).ToList();
            return users;
        }
        public async Task Register(CreateUserDto dto)
        {
            var user = new UserDbEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                UserName = dto.Email,
                Email = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
               
        }
    }
}
