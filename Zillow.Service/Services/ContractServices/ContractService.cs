using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Dto;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Core.Enum;
using Zillow.Core.ViewModel;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;
using Zillow.Service.Services.FileServices;

namespace Zillow.Service.Services.ContractServices
{
    public class ContractService : IContractService
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ContractService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int)Math.Ceiling(await _dbContext.Contract.CountAsync() / (double)pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var contracts = await _dbContext.Contract
                .Include(x => x.Customer)
                .Include(x => x.RealEstate)
                .Skip(skipVal).Take(pageSize)
                .ToListAsync();

            var contractsViewModel = _mapper.Map<List<ContractViewModel>>(contracts);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = contractsViewModel
            };
        }

        public async Task<ContractViewModel> Get(int id)
        {
            var contract = await _dbContext.Contract
                .Include(x => x.Customer)
                .Include(x => x.RealEstate)
                .SingleOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<ContractViewModel>(contract);
        }

        public async Task<int> Create(CreateContractDto dto, string userId)
        {
            var createdContract = _mapper.Map<ContractDbEntity>(dto);

            createdContract.CreatedBy = userId;

            await _dbContext.Contract.AddAsync(createdContract);
            await _dbContext.SaveChangesAsync();

            return createdContract.Id;

        }

        public async Task<int> Update(UpdateContractDto dto, string userId)
        {
            var oldContract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.Id == dto.Id);

            var updatedContract = _mapper.Map(dto, oldContract);

            updatedContract.UpdatedAt = DateTime.Now;
            updatedContract.UpdatedBy = userId;

            _dbContext.Contract.Update(updatedContract);
            await _dbContext.SaveChangesAsync();

            return updatedContract.Id;
        }

        public async Task<int> Delete(int id, string userId)
        {
            var deletedContract = await _dbContext.Contract.SingleOrDefaultAsync(x => x.Id == id);

            deletedContract.UpdatedAt = DateTime.Now;
            deletedContract.UpdatedBy = userId;
            deletedContract.IsDelete = true;

            _dbContext.Contract.Update(deletedContract);
            await _dbContext.SaveChangesAsync();

            return deletedContract.Id;
        }
    }
}
