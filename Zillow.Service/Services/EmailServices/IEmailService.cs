using System.Threading.Tasks;

namespace Zillow.Service.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmail(string sendTo, string msgSubject, string msgContent);
    }
}