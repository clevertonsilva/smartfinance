namespace smartfinance.Domain.Interfaces.Services.Authentication
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHTML);
    }
}
