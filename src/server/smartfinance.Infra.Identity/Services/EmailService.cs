using Microsoft.Extensions.Configuration;
using smartfinance.Domain.Interfaces.Services.Authentication;
using System.Net;
using System.Net.Mail;

namespace smartfinance.Infra.Identity.Services
{
    public class EmailService : IEmailService
    { 
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML)
        {
            string _mailServer = _config["EmailSettings:MailServer"];
            string _fromEmail = _config["EmailSettings:FromEmail"];
            string _password = _config["EmailSettings:Password"];
            
            if (!int.TryParse(_config["EmailSettings:MailPort"], out int _port))
            {
                _port = 587;
            }

            var _client = new SmtpClient(_mailServer, _port)
            {
                Credentials = new NetworkCredential(_fromEmail, _password),
                EnableSsl = true,
            };

            var _mailMessage = new MailMessage(_fromEmail, toEmail, subject, body)
            {
                IsBodyHtml = isBodyHTML
            };

            return _client.SendMailAsync(_mailMessage);

        }
    }
}
