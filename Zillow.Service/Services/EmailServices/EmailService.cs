using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Zillow.Service.Services.EmailServices
{
    public class EmailService : IEmailService
    {

        private readonly IWebHostEnvironment _webHost;

        public EmailService(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }

        public async Task SendEmail(string sendTo, string msgSubject, string msgContent)
        {

            var filePath = Path.Combine(_webHost.WebRootPath, "Templates/email.html");

            var body = await File.OpenText(filePath).ReadToEndAsync();

            var message = new MailMessage
            {
                To = {new MailAddress(sendTo)},
                From = new MailAddress("zillow.co@gmail.com","Zillow Company"),
                Subject = msgSubject,
                Body = body,
                IsBodyHtml = true
            };

            using var client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("zillow.co@gmail.com","Myzillowaccount"),
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };

            await client.SendMailAsync(message);
        }
    }
}