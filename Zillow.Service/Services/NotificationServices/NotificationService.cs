using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirebaseAdmin.Messaging;
using Zillow.Core.Dto.CreateDto;

namespace Zillow.Service.Services.NotificationServices
{
    public class NotificationService : INotificationService
    {

        private readonly FirebaseMessaging _messaging;
        

        public NotificationService()
        {
            _messaging = FirebaseMessaging.DefaultInstance;
            
        }

        public List<Message> CreateNotifications(CreateMessageDto dto, List<string> tokens)
        {
            return tokens.Select(token => new Message()
                {
                    Token = token,
                    Data = new Dictionary<string, string>()
                    {
                        {"Title", dto.Title},
                        {"Body", dto.Body}, 
                        {"Action", dto.Action},
                        {"ActionId", dto.ActionId.ToString()},
                    },
                })
                .ToList();
        }

        public async Task<int> PushNotifications(List<Message> messages)
        {
          var result = await _messaging.SendAllAsync(messages);

           return result.SuccessCount;
        }

        
    }
}
