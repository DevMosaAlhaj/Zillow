using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;


namespace Zillow.Service.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddressService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int) Math.Ceiling(await _dbContext.Address.CountAsync() / (double) pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var addresses = await _dbContext.Address
                .Include(x => x.RealEstates)
                .Skip(skipVal).Take(pageSize)
                .ToListAsync();

            var addressesViewModel = _mapper.Map<List<AddressViewModel>>(addresses);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = addressesViewModel
            };
        }

        public async Task<AddressViewModel> Get(int id)
        {
            var address = await _dbContext.Address
                .Include(x => x.RealEstates)
                .SingleOrDefaultAsync(x => x.Id == id);
            
            return _mapper.Map<AddressViewModel>(address);
        }
        
        public async Task<int> Create(CreateAddressDto dto, string userId)
        {
            var createdAddress = _mapper.Map<AddressDbEntity>(dto);

            createdAddress.CreatedBy = userId;

            await _dbContext.Address.AddAsync(createdAddress);
            await _dbContext.SaveChangesAsync();

            return createdAddress.Id;
        }

        public async Task<int> Update(int id,UpdateAddressDto dto, string userId)
        {
            var oldAddress = await _dbContext.Address.SingleOrDefaultAsync(x => x.Id == id);

            var updatedAddress = _mapper.Map(dto,oldAddress);

            updatedAddress.UpdatedAt = DateTime.Now;
            updatedAddress.UpdatedBy = userId;

            _dbContext.Address.Update(updatedAddress);
            await _dbContext.SaveChangesAsync();

            return updatedAddress.Id;
        }

        public async Task<int> Delete(int id, string userId)
        {
            var deletedAddress = await _dbContext.Address.SingleOrDefaultAsync(x => x.Id == id);

            deletedAddress.UpdatedAt = DateTime.Now;
            deletedAddress.UpdatedBy = userId;
            deletedAddress.IsDelete = true;

            _dbContext.Address.Update(deletedAddress);
            await _dbContext.SaveChangesAsync();

            return deletedAddress.Id;
        }
    }
}