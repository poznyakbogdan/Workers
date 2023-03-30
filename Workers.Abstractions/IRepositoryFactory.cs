namespace Workers.Abstractions;

public interface IRepositoryFactory
{
    IRepository<TEntity> Create<TEntity>() where TEntity: class;
}