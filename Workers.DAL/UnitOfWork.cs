using Workers.Abstractions;

namespace Workers.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppContext _context;
    private readonly IRepositoryFactory _repositoryFactory;

    public UnitOfWork(AppContext context, IRepositoryFactory repositoryFactory)
    {
        _context = context;
        _repositoryFactory = repositoryFactory;
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return _repositoryFactory.Create<TEntity>();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}