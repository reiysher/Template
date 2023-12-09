using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace IntegrationTests.Utils.SQLite;

public class DbContextOptionsDisposable<T>(DbContextOptions<T> baseOptions)
    : DbContextOptions<T>(new ReadOnlyDictionary<Type, IDbContextOptionsExtension>(
        baseOptions.Extensions.ToDictionary(x => x.GetType()))),
    IDisposable
    where T : DbContext
{
    private bool _stopNextDispose;
    private bool _turnOffDispose;
    private readonly DbConnection? _connection = RelationalOptionsExtension.Extract(baseOptions).Connection;

    public void StopNextDispose()
    {
        _stopNextDispose = true;
    }

    public void TurnOffDispose()
    {
        _turnOffDispose = true;
    }

    public void ManualDispose()
    {
        _turnOffDispose = false;
        _stopNextDispose = false;
        Dispose();
    }

    public void Dispose()
    {
        if (!_stopNextDispose && !_turnOffDispose)
            _connection?.Dispose();
        _stopNextDispose = false;
    }
}
