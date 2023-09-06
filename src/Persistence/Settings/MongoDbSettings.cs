using System.ComponentModel.DataAnnotations;

namespace Persistence.Settings;

public class MongoDbSettings
{
    [Required]
    public string? ConnectionString { get; set; }

    [Required]
    public string? DatabaseName { get; set; }
}