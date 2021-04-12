using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Dto;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;
using Zillow.Service.Services.FileServices;
using Zillow.Service.Services.NotificationServices;

namespace Zillow.Service.Services.RealEstateServices
{
    public class RealEstatesService : IRealEstatesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly INotificationService _notificationService;

        public RealEstatesService(ApplicationDbContext dbContext, IMapper mapper, IFileService fileService, INotificationService notificationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _notificationService = notificationService;
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int)Math.Ceiling(await _dbContext.RealEstate.CountAsync() / (double)pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var RealEstates = await _dbContext.RealEstate
                .Include(x => x.Category).Include(x => x.Address)
                .Skip(skipVal).Take(pageSize).ToListAsync();

            var RealEstatesViewModel = _mapper.Map<List<RealEstateViewModel>>(RealEstates);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = RealEstatesViewModel
            };
        }
        public async Task<RealEstateViewModel> Get(int id)
        {
            var realEstate = await _dbContext.RealEstate.Include(x => x.Category).Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Id == id);

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

            deletedRealEstate.UpdatedAt = DateTime.Now;
            deletedRealEstate.UpdatedBy = userId;
            deletedRealEstate.IsDelete = true;

            _dbContext.RealEstate.Update(deletedRealEstate);
            await _dbContext.SaveChangesAsync();

            return deletedRealEstate.Id;
        }



    }
}
