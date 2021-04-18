using System;
using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Constant;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.Exceptions;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;
using Zillow.Service.Services.EmailServices;

namespace Zillow.Service.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserDbEntity> _manager;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly EntityNotFoundException _notFoundException;

        public UserService(UserManager<UserDbEntity> manager, ApplicationDbContext dbContext, IMapper mapper,
            IEmailService emailService)
        {
            _manager = manager;
            _dbContext = dbContext;
            _mapper = mapper;
            _emailService = emailService;
            _notFoundException = new EntityNotFoundException("User");
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

            if (user == null) throw _notFoundException;

            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<string> Create(CreateUserDto dto)
        {
            if (!IsEmailValid(dto.Email))
                throw new UserRegistrationException(ExceptionMessage.InvalidEmail);

            var createdUser = _mapper.Map<UserDbEntity>(dto);

            createdUser.UserName = createdUser.Email;

            // if user added not successfully Throw UserRegistrationException 

            var result = await _manager.CreateAsync(createdUser, dto.Password);

            if (result.Succeeded)
            {
                await _emailService.SendEmail(createdUser.Email, "Welcome Email", "");
                return createdUser.Id;
            }

            var errorsMessage =
                result.Errors.Aggregate("", (current, error) => current + $"\n{error.Code}");


            throw new UserRegistrationException(errorsMessage);
        }

        public async Task<string> Update(string id, UpdateUserDto dto, string userId)
        {
            var oldUser = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (oldUser == null) throw _notFoundException;

            // Note : We Don't Map (Email) for User , it's unchangeable
            // See MapperProfile Line 63

            if (!id.Equals(dto.Id))
                throw new UpdateEntityException(ExceptionMessage.UpdateEntityIdError);

            var updatedUser = _mapper.Map(dto, oldUser);

            updatedUser.UpdatedAt = DateTime.Now;
            updatedUser.UpdatedBy = userId;

            await _manager.UpdateAsync(updatedUser);

            return updatedUser.Id;
        }

        public async Task<string> Delete(string id, string userId)
        {
            var deletedUser = await _dbContext.Users.SingleOrDefaultAsync(x => x.Id.Equals(id));

            if (deletedUser == null) throw _notFoundException;

            deletedUser.UpdatedAt = DateTime.Now;
            deletedUser.UpdatedBy = userId;

            await _manager.UpdateAsync(deletedUser);

            return deletedUser.Id;
        }

        private static bool IsEmailValid(string email)
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var match = regex.Match(email);
            return match.Success;
        }
    }
}