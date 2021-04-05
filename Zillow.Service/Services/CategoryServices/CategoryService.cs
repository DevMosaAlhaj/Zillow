using Zillow.Core.Dto;
using Zillow.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;
using Zillow.Data.Data;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int) Math.Ceiling(await _dbContext.Category.CountAsync() / (double) pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var categories = await _dbContext.Category
                .Include(x => x.RealEstates)
                .Skip(skipVal).Take(pageSize)
                .ToListAsync();

            var categoriesViewModel = _mapper.Map<List<CategoryViewModel>>(categories);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = categoriesViewModel
            };
        }

        public async Task<CategoryViewModel> Get(int id)
        {
            var category = await _dbContext.Category
                .Include(x => x.RealEstates)
                .SingleOrDefaultAsync(x=> x.Id == id);

            return _mapper.Map<CategoryViewModel>(category);
        }

        public async Task<int> Create(CreateCategoryDto dto, string userId)
        {
            var createdCategory = _mapper.Map<CategoryDbEntity>(dto);

            createdCategory.CreatedBy = userId;

            await _dbContext.Category.AddAsync(createdCategory);
            await _dbContext.SaveChangesAsync();

            return createdCategory.Id;
        }

        public async Task<int> Update( UpdateCategoryDto dto, string userId)
        {
            var oldCategory = await _dbContext.Category.SingleOrDefaultAsync(x => x.Id == dto.Id);

            var updatedCategory = _mapper.Map(dto, oldCategory);

            updatedCategory.UpdatedAt = DateTime.Now;
            updatedCategory.UpdatedBy = userId;

            _dbContext.Category.Update(updatedCategory);
            await _dbContext.SaveChangesAsync();

            return updatedCategory.Id;
        }

        public async Task<int> Delete(int id, string userId)
        {
            var deletedCategory = await _dbContext.Category.SingleOrDefaultAsync(x => x.Id == id);

            deletedCategory.UpdatedAt = DateTime.Now;
            deletedCategory.UpdatedBy = userId;
            deletedCategory.IsDelete = true;

            _dbContext.Category.Update(deletedCategory);
            await _dbContext.SaveChangesAsync();
            
            return deletedCategory.Id;
        }
    }
}