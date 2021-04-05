using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Zillow.Service.Services.FileServices
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folderName);

    }
}
