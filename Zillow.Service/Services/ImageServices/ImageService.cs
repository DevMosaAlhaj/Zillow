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
using Zillow.Service.Services.FileServices;

namespace Zillow.Service.Services.ImageServices
{
    public class ImageService : IImageService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly EntityNotFoundException _notFoundException;

        public ImageService(ApplicationDbContext dbContext, IFileService fileService, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
            _notFoundException = new EntityNotFoundException("Image");
        }

        public async Task<PagingViewModel> GetAll(int page, int pageSize)
        {
            var pagesCount = (int) Math.Ceiling(await _dbContext.Images.CountAsync() / (double) pageSize);

            if (page > pagesCount || page < 1)
                page = 1;

            var skipVal = (page - 1) * pageSize;

            var images = await _dbContext.Images
                .Include(x => x.RealEstate)
                .Skip(skipVal).Take(pageSize).ToListAsync();

            var imagesViewModel = _mapper.Map<List<ImageViewModel>>(images);

            return new PagingViewModel()
            {
                CurrentPage = page,
                PagesCount = pagesCount,
                Data = imagesViewModel
            };
        }

        public async Task<ImageViewModel> Get(int id)
        {
            var image = await _dbContext.Images
                .Include(x => x.RealEstate)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (image == null) throw _notFoundException;
            
            return _mapper.Map<ImageViewModel>(image);
        }

        public async Task<int> Create(CreateImageDto dto, string userId)
        {
            var createdImage = _mapper.Map<ImageDbEntity>(dto);

            createdImage.CreatedBy = userId;
            createdImage.ImageUrl = await _fileService.SaveFile(dto.Image, $"RealStateImages{dto.RealEstateId}");

            await _dbContext.Images.AddAsync(createdImage);
            await _dbContext.SaveChangesAsync();

            return createdImage.Id;
        }

        public async Task<int> Update(int id, UpdateImageDto dto, string userId)
        {
            var oldImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.Id == id);

            if (oldImage == null) throw _notFoundException;
            
            if (id != dto.Id)
                throw new UpdateEntityException(ExceptionMessage.UpdateEntityIdError);
            
            var updatedImage = _mapper.Map(dto, oldImage);

            updatedImage.UpdatedAt = DateTime.Now;
            updatedImage.UpdatedBy = userId;
            updatedImage.ImageUrl = await _fileService.SaveFile(dto.Image, $"RealStateImages{oldImage.RealEstateId}");

            _dbContext.Images.Update(updatedImage);
            await _dbContext.SaveChangesAsync();

            return updatedImage.Id;
        }

        public async Task<int> Delete(int id, string userId)
        {
            var deletedImage = await _dbContext.Images.SingleOrDefaultAsync(x => x.Id == id);

            if (deletedImage == null) throw _notFoundException;
            
            deletedImage.UpdatedAt = DateTime.Now;
            deletedImage.UpdatedBy = userId;
            deletedImage.IsDelete = true;

            _dbContext.Images.Update(deletedImage);
            await _dbContext.SaveChangesAsync();

            return deletedImage.Id;
        }
    }
}