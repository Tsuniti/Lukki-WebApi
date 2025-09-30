using Lukki.Application.Common.Interfaces.Services.Payment;
using Lukki.Application.Orders.Common;
using Microsoft.Extensions.Options;
using Stripe;

namespace Lukki.Infrastructure.External.StripePayment;

public class StripePaymentService : IPaymentService
{
    public StripePaymentService(IOptions<StripePaymentServiceSettings> stripeOptions)
    {
        StripeConfiguration.ApiKey = stripeOptions.Value.SecretKey;
    }

    public async Task<string> CreatePaymentIntentAsync(decimal amount, string currency)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = (long)(amount * 100),
            Currency = currency,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
                AllowRedirects = "never" 
            },
        };

        var service = new PaymentIntentService();
        var paymentIntent = await service.CreateAsync(options);
        return paymentIntent.Id;
    }


    public async Task<ConfirmPaymentResult> ConfirmPaymentAsync(string paymentIntentId, string paymentMethodToken)
    {
        try
        {
            var paymentMethodService = new PaymentMethodService();
            var paymentMethodOptions = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Token = paymentMethodToken
                }
            };
        
            var paymentMethod = await paymentMethodService.CreateAsync(paymentMethodOptions);
            
            var piService = new PaymentIntentService();
            var pi = await piService.ConfirmAsync(paymentIntentId, new PaymentIntentConfirmOptions
            {
                PaymentMethod = paymentMethod.Id
            });

            var result = new ConfirmPaymentResult(
                Status: pi.Status,
                Amount: pi.Amount / 100m,
                Currency: pi.Currency,
                PaymentIntentId: pi.Id
            );
        
            return result;
        }
        catch (StripeException ex)
        {
            throw new PaymentException("Error when confirming payment", ex);
        }
    }

    
    
}