using System.Linq.Expressions;

namespace Workers.Abstractions;

public interface IRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);
    Task CreateAsync(TEntity entity);
    void Remove(TEntity entity);
    IQueryable<TEntity> AsQueryable();
}