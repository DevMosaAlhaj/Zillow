using System.Threading.Tasks;
using Zillow.Core.Dto.AuthDto;
using Zillow.Core.ViewModel;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.AuthServices
{
    public interface IAuthService
    {

        Task<LoginResponseViewModel> Login(LoginDto dto);

        Task<string> Logout(string token);

        Task<LoginResponseViewModel> RefreshToken(string refreshToken);

        Task<TokenViewModel> GenerationAccessToken(UserDbEntity user);

        Task<string> RegisterFcmToken(string userId, string userFcmToken);

    }
}
