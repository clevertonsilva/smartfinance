namespace smartfinance.Domain.Interfaces.Utils
{
    public interface IUnitOfWork
    {
        Task<bool> Commit(CancellationToken cancellationToken = default);
    }
}
