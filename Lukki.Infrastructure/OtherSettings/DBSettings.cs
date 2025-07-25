namespace Lukki.Infrastructure.OtherSettings;

public class DBSettings
{
    public const string SectionName = "DBSettings";
    
    public string ConnectionString { get; init; } = null!;
}