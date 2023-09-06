using Ardalis.GuardClauses;
using Domain.Authors.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Authors.Guards;

public static class AuthorGuard
{
    public static Author AuthorNotFound(
        this IGuardClause guardClause,
        [NotNull] Author? author,
        Guid authorId)
    {
        if (author is null)
        {
            throw new AuthorNotFoundException(authorId);
        }

        return author;
    }
}
