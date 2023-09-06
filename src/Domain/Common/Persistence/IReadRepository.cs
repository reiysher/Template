namespace Domain.Common.Persistence;

public interface IReadRepository<TEntity> : IRepository<TEntity>
    where TEntity : IAggregateRoot
{
}
