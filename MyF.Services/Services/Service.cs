using MyF.Infrastructure.Data;
using SqlSugar;
using System.Linq.Expressions;

namespace MyF.Services
{
    public class Service<T> : IService<T> where T : class, IEntity, new()
    {
        protected readonly SqlSugarDbContext _dbContext;
        protected readonly ISqlSugarClient _db;

        public Service(SqlSugarDbContext dbContext)
        {
            _dbContext = dbContext;
            _db = dbContext.GetDbClient();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _db.Queryable<T>().InSingleAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Queryable<T>().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _db.Queryable<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _db.Insertable(entity).ExecuteCommandAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _db.Updateable(entity).ExecuteCommandAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _db.Deleteable<T>().In(id).ExecuteCommandAsync();
        }
    }
}
