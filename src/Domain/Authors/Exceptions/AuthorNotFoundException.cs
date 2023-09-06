using Domain.Common;
using Domain.Notes;

namespace Domain.Authors.Exceptions;

public sealed class AuthorNotFoundException : DomainException
{
    public Guid AuthorId { get; private set; }

    public AuthorNotFoundException(Guid authorId)
    {
        AuthorId = authorId;
    }
}
