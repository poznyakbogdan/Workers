using Microsoft.Extensions.DependencyInjection;
using Workers.Abstractions;

namespace Workers.DAL;

public class RepositoryFactory : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider;

    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public IRepository<TEntity> Create<TEntity>() where TEntity: class
    {
        return _serviceProvider.GetService<IRepository<TEntity>>();
    }
}