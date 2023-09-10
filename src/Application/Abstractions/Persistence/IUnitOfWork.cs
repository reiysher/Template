using System.Data;

namespace Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> CommitAsync(
        CancellationToken cancellationToken = default);
}
