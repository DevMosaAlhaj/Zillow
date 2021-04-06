using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;
using Zillow.Core.Dto.UpdateDto;

namespace Zillow.Service.Services.UserServices
{
  public interface IUserService
  {
      Task<PagingViewModel> GetAll(int page, int pageSize);

      Task<UserViewModel> Get(string id);

      Task<string> Create(CreateUserDto dto);
      
      Task<string> Update(string id , UpdateUserDto dto , string userId );

      Task<string> Delete(string id, string userId);
  }
}
