using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace IntegrationTests.Utils.SQLite;

public static class SQLiteInMemory
{
    public static DbContextOptionsDisposable<T> CreateOptions<T>(
        Action<DbContextOptionsBuilder<T>>? builder = null)
        where T : DbContext
    {
        return new DbContextOptionsDisposable<T>(SetupOptions<T>(builder).Options);

    }

    private static DbContextOptionsBuilder<T> SetupOptions<T>(
        Action<DbContextOptionsBuilder<T>>? extraOptions)
        where T : DbContext
    {
        var connectionStringBuilder = new SqliteConnectionStringBuilder
        {
            DataSource = ":memory:"
        };

        var connextionString = connectionStringBuilder.ToString();
        var connection = new SqliteConnection(connextionString);
        connection.Open();

        var builder = new DbContextOptionsBuilder<T>();
        builder.UseSqlite(connection);
        builder.EnableDetailedErrors();
        builder.EnableSensitiveDataLogging();
        builder.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        extraOptions?.Invoke(builder);

        return builder;
    }
}
