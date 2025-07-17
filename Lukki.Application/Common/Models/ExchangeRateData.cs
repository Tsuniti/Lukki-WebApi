namespace Lukki.Application.Common.Models;

public class ExchangeRateData
{
    public string BaseCurrency { get; set; } = default!;
    public Dictionary<string, decimal> Rates { get; set; } = new();
    public DateTime LastUpdated { get; set; }
}