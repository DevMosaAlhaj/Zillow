using System;
using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.UserServices
{
    public class UserService : IUserService
    {

        private readonly UserManager<UserDbEntity> _manager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserDbEntity> manager, ApplicationDbContext dbContext, IMapper mapper)
        {
            _manager = manager;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int) Math.Ceiling(await _dbContext.Users.CountAsync() / (double) pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pagesCount;

            var users = await _dbContext.Users
                .Skip(skipVal).Take(pageSize).ToListAsync();


            var usersViewModel = _mapper.Map<List<UserViewModel>>(users);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = usersViewModel
            };
        }

        public async Task<UserViewModel> Get(string id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id.Equals(id));

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<string> Create(CreateUserDto dto)
        {
            var createdUser = _mapper.Map<UserDbEntity>(dto);

            createdUser.UserName = createdUser.Email;
            
            // if add user not successfully Throw UserRegistrationException 

            await _manager.CreateAsync(createdUser, dto.Password);

            return createdUser.Id;
        }

        public async Task<string> Update(string id, UpdateUserDto dto, string userId)
        {
            var oldUser = await _dbContext.Users.SingleOrDefaultAsync(x=> x.Id.Equals(id));

            // Note : We Not Map (Email) for User , it's unchangeable
            // See MapperProfile Line 63
            
            var updatedUser = _mapper.Map(dto, oldUser);
            
            updatedUser.UpdatedAt = DateTime.Now;
            updatedUser.UpdatedBy = userId;

            await _manager.UpdateAsync(updatedUser);

            return updatedUser.Id;
        }

        public async Task<string> Delete(string id, string userId)
        {
            var deletedUser = await _dbContext.Users.SingleOrDefaultAsync(x=> x.Id.Equals(id));
            
            deletedUser.UpdatedAt = DateTime.Now;
            deletedUser.UpdatedBy = userId;

            await _manager.UpdateAsync(deletedUser);

            return deletedUser.Id;
        }
    }
}
