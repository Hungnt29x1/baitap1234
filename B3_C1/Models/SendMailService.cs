using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;

namespace B3_C1.Models
{
    public class SendMailService: ISendMailService
    {
        private readonly IConfiguration _configuration;

        public SendMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var smtpServer = _configuration["Email:SmtpServer"];
            var smtpPort = _configuration["Email:SmtpPort"];
            var smtpUsername = _configuration["Email:SmtpUsername"];
            var smtpPassword = _configuration["Email:SmtpPassword"];

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Your Name", smtpUsername));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect(smtpServer, int.Parse(smtpPort), SecureSocketOptions.StartTls);
                client.Authenticate(smtpUsername, smtpPassword);
                await client.SendAsync(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
