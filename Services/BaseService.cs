using Microsoft.EntityFrameworkCore;
using TaskManager.Data;

namespace TaskManager.Services
{
    public abstract class BaseService<TEntity, TCreateModel> : IBaseService<TEntity, TCreateModel>
        where TEntity : class
        where TCreateModel : class
    {
        protected readonly TaskDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected BaseService(TaskDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> AddAsync(TCreateModel model)
        {
            var entity = MapToEntity(model);
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity?> GetByIdASync(int id)
            => await _dbSet.FindAsync(id);

        public virtual async Task<List<TEntity>> GetAllAsync()
            => await _dbSet.ToListAsync();

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity is null)
                return;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        protected abstract TEntity MapToEntity(TCreateModel model);
    }
}
