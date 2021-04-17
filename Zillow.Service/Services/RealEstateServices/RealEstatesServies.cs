using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Constant;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.Exceptions;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;
using Zillow.Service.Services.NotificationServices;

namespace Zillow.Service.Services.RealEstateServices
{
    public class RealEstatesService : IRealEstatesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;
        private readonly EntityNotFoundException _notFoundException;

        public RealEstatesService(ApplicationDbContext dbContext, IMapper mapper, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _notificationService = notificationService;
            _notFoundException = new EntityNotFoundException("Real Estate");
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int)Math.Ceiling(await _dbContext.RealEstate.CountAsync() / (double)pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var realEstates = await _dbContext.RealEstate
                .Include(x => x.Category).Include(x => x.Address)
                .Skip(skipVal).Take(pageSize).ToListAsync();

            var realEstatesViewModel = _mapper.Map<List<RealEstateViewModel>>(realEstates);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = realEstatesViewModel
            };
        }
        public async Task<RealEstateViewModel> Get(int id)
        {
            var realEstate = await _dbContext.RealEstate.Include(x => x.Category).Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (realEstate == null) throw _notFoundException;
            
            return _mapper.Map<RealEstateViewModel>(realEstate);
        }
        public async Task<int> Create(CreateRealEstatesDto dto, string userId)
        {
            var createdRealEstate = _mapper.Map<RealEstateDbEntity>(dto);

            createdRealEstate.CreatedBy = userId;

            await _dbContext.RealEstate.AddAsync(createdRealEstate);
            await _dbContext.SaveChangesAsync();

            await SendNotificationToUsers(createdRealEstate);
            
            return createdRealEstate.Id;
        }

        private async Task SendNotificationToUsers(RealEstateDbEntity realEstate)
        {
            var usersFcmToken = await _dbContext.Users
                .Where(x => !string.IsNullOrEmpty(x.FcmToken))
                .Select(x => x.FcmToken).ToListAsync();

            var messageBody = $"{realEstate.Name}" +
                              $"Added in {realEstate.Address}" +
                              $"\n {realEstate.Description}" +
                              $"At {realEstate.CreatedAt}";

            var authorName = await _dbContext.Users
                .SingleOrDefaultAsync(x => x.Id.Equals(realEstate.CreatedBy));

            var messageDto = new CreateMessageDto()
            {
                Title = $"New Post From {authorName.FirstName}",
                Body = messageBody,
                Action = "NewRealEstate",
                ActionId = realEstate.Id
            };

            var messages = _notificationService.CreateNotifications(messageDto, usersFcmToken);

            await _notificationService.PushNotifications(messages);
        }

        public async Task<int> Update(int id ,UpdateRealEstatesDto dto, string userId)
        {
            var oldRealEstate = await _dbContext.RealEstate.SingleOrDefaultAsync(x => x.Id == id);

            if (oldRealEstate == null) throw _notFoundException;
            
            if (id != dto.Id)
                throw new UpdateEntityException(ExceptionMessage.UpdateEntityIdError);
            
            var updatedRealEstate = _mapper.Map(dto, oldRealEstate);

            updatedRealEstate.UpdatedAt = DateTime.Now;
            updatedRealEstate.UpdatedBy = userId;

            _dbContext.RealEstate.Update(updatedRealEstate);
            await _dbContext.SaveChangesAsync();

            return updatedRealEstate.Id;
        }
        public async Task<int> Delete(int id, string userId)
        {
            var deletedRealEstate = await _dbContext.RealEstate.SingleOrDefaultAsync(x => x.Id == id);

            if (deletedRealEstate == null) throw _notFoundException;
            
            deletedRealEstate.UpdatedAt = DateTime.Now;
            deletedRealEstate.UpdatedBy = userId;
            deletedRealEstate.IsDelete = true;

            _dbContext.RealEstate.Update(deletedRealEstate);
            await _dbContext.SaveChangesAsync();

            return deletedRealEstate.Id;
        }



    }
}
