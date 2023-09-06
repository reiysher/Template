using System.ComponentModel.DataAnnotations;

namespace Persistence.Settings;

public class DatabaseSettings
{
    public const string SectionName = nameof(DatabaseSettings);

    [Required]
    public PostgreSqlSettings PostgreSql { get; set; } = default!;

    [Required]
    public MongoDbSettings MongoDb { get; set; } = default!;
}
