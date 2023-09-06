using Domain.Authors;

namespace Application.Features.Notes.Services;

public interface INoteService
{
    Task DeleteByAuthodId(Guid authorId, CancellationToken cancellationToken);
}
