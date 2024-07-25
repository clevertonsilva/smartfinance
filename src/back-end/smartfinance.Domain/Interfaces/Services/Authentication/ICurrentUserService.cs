namespace smartfinance.Domain.Interfaces.Services.Authentication
{
    public interface ICurrentUserService
    {
        string Name { get; }
        bool IsAuthenticated { get; }
    }
}
