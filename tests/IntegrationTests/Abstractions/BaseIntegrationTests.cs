using Infrastructure.Persistence.Contexts;
using IntegrationTests.Utils.SQLite;

namespace IntegrationTests.Abstractions;

public abstract class BaseIntegrationTests : IDisposable
{
    private protected readonly DbContextOptionsDisposable<ApplicationDbContext> _dbContextOptions;

    public BaseIntegrationTests()
    {
        _dbContextOptions = SQLiteInMemory.CreateOptions<ApplicationDbContext>();
        _dbContextOptions.TurnOffDispose();

        using var dbContext = new TestsDbContext(_dbContextOptions);
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _dbContextOptions.ManualDispose();
    }
}
