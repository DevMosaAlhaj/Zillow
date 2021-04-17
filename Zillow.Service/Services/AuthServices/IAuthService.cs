using System.Threading.Tasks;
using Zillow.Core.Dto.AuthDto;
using Zillow.Core.ViewModel;
using Zillow.Data.DbEntity;

namespace Zillow.Service.Services.AuthServices
{
    public interface IAuthService
    {

        Task<LoginResponseViewModel> Login(LoginDto dto);

        Task<string> Logout(string userId);

        LoginResponseViewModel RefreshToken(string refreshToken);

        TokenViewModel GenerationAccessToken(UserDbEntity user);

        Task<string> RegisterFcmToken(string userId, string userFcmToken);

    }
}
