namespace Domain.Common;

public interface IProjection
{
}

public interface IProjection<TId> : IProjection
    where TId : notnull
{
    TId Id { get; }
}
