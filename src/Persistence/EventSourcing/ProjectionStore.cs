using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.EventSourcing;

internal class ProjectionStore<TProjection, TId> : IProjectionStore<TProjection, TId>
    where TProjection : class, IProjection<TId>
    where TId : notnull
{
    private readonly ApplicationDbContext _dbContext;

    public ProjectionStore(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TProjection projection, CancellationToken cancellationToken)
    {
        await _dbContext
            .Set<TProjection>()
            .AddAsync(projection, cancellationToken);
    }

    public async Task<TProjection?> GetByIdAsync(TId projectionId, CancellationToken cancellationToken)
    {
        return await _dbContext
            .Set<TProjection>()
            .FirstOrDefaultAsync(p => projectionId.Equals(p.Id), cancellationToken);
    }
}
