using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using Zillow.Core.Dto.CreateDto;

namespace Zillow.Service.Services.NotificationServices
{
    public interface INotificationService
    {
        
        Task<int> PushNotifications(List<Message> messages);

        List<Message> CreateNotifications(CreateMessageDto dto, List<string> tokens);

    }
}
