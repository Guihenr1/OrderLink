using OrderLink.Sync.Core.Models;

namespace OrderLink.Sync.Core.Data
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
