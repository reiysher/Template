using System.ComponentModel.DataAnnotations;

namespace Persistence.Settings;

public class PostgreSqlSettings
{
    [Required]
    public string? ConnectionString { get; set; }

    [Required]
    public int? CommandTimeoutInSeconds { get; set; }
}
