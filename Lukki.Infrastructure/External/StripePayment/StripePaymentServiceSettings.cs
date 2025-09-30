namespace Lukki.Infrastructure.External.StripePayment;

public class StripePaymentServiceSettings
{
    public const string SectionName = "Stripe";
    public string SecretKey  { get; set; } = null!;
}