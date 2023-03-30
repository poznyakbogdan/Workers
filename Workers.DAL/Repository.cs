using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Workers.Abstractions;

namespace Workers.DAL;

public class Repository<TEntity> : IRepository<TEntity> where TEntity: class
{
    private readonly DbSet<TEntity> _entities;

    public Repository(AppContext context)
    {
        _entities = context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _entities.Where(expression).ToListAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
    }
        
    public void Remove(TEntity entity)
    {
        _entities.Remove(entity);
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _entities.AsQueryable();
    }
}