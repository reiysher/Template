namespace Domain.Common;

public interface IProjectionStore<TProjection, TId>
    where TProjection : IProjection<TId>
    where TId : notnull
{
    Task AddAsync(TProjection projection, CancellationToken cancellationToken);

    Task<TProjection?> GetByIdAsync(TId projectionId, CancellationToken cancellationToken);
}
