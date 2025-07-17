namespace Lukki.Infrastructure.Persistence.Models;

public class ExchangeRate
{
    public int Id { get; set; }
    public string BaseCurrency { get; set; }
    public Dictionary<string, decimal> Rates { get; set; } = new();
    public DateTime LastUpdated { get; set; }
}