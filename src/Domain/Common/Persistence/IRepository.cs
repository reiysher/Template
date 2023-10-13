namespace Domain.Common.Persistence;

public interface IRepository<TEntity>
    where TEntity : IAggregateRoot
{
}
