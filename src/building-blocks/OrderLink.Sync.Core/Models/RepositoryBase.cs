using Microsoft.EntityFrameworkCore;
using OrderLink.Sync.Core.Data;

namespace OrderLink.Sync.Core.Models
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : EntityBase
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }
    }
}
