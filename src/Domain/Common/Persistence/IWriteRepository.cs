namespace Domain.Common.Persistence;

public interface IWriteRepository<TEntity> : IRepository<TEntity>
    where TEntity : IAggregateRoot
{
}
