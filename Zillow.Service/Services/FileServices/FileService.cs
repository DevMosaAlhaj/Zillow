using System;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Zillow.Service.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public FileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFile(IFormFile file,string folderName)
        {
            string fileName = null;
            if (file == null || file.Length <= 0) return null; // Trow EmptyFileException
            var uploadPath = Path.Combine(_env.WebRootPath, folderName);
            
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            
            fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
            
            await using var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create);
            await file.CopyToAsync(fileStream);
            return $"{folderName}/{fileName}";
        }
      
    }
}
