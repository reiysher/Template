using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace IntegrationTests;

public abstract class BaseIntegrationTests
{
    public BaseIntegrationTests()
    {
        var dbContext = GetDbContext();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }

    private protected static ApplicationDbContext GetDbContext()
    {
        var connectionString = "Host=localhost;Port=5432;Database=local_template_db_tests;Username=postgres;Password=pa55word";
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseNpgsql(connectionString);

        return new ApplicationDbContext(builder.Options);
    }
}
