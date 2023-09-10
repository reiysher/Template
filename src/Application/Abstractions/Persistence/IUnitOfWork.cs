﻿namespace Application.Abstractions.Persistence;

public interface IUnitOfWork
{
    Task<int> SaveChanges(
        CancellationToken cancellationToken = default);

    //Task<IDisposable> BeginTransactionAsync(
    //    IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
    //    CancellationToken cancellationToken = default);

    //Task<IDisposable> BeginTransactionAsync(
    //    IsolationLevel isolationLevel = IsolationLevel.ReadCommitted,
    //    string? lockName = null,
    //    CancellationToken cancellationToken = default);

    //Task CommitTransactionAsync(
    //    CancellationToken cancellationToken = default);
}
