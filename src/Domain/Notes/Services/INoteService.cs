namespace Domain.Notes.Services;

public interface INoteService
{
    Task DeleteByAuthodId(Guid authorId, CancellationToken cancellationToken);
}
