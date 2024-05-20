namespace OrderLink.Sync.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
        bool Rollback();
    }
}
