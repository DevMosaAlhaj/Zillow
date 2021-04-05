using Zillow.Core.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zillow.Core.Dto.CreateDto;

namespace Zillow.Service.Services.UserServices
{
  public interface IUserService
    {
        List<UserViewModel> GetAll();
        Task Register(CreateUserDto dto);
    }
}
