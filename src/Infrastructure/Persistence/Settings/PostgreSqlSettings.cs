using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Settings;

public class PostgreSqlSettings
{
    [Required]
    public string? ConnectionString { get; set; }

    [Required]
    public int? CommandTimeoutInSeconds { get; set; }
}
