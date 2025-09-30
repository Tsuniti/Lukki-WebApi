using Lukki.Application.Common.Interfaces.Services.Payment;
using Lukki.Application.Orders.Common;
using MediatR;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;

namespace Lukki.Application.Orders.Commands.ConfirmPayment;

public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, ErrorOr<ConfirmPaymentResult>>
{
    
    private readonly IPaymentService _paymentService;
    private readonly IOrderRepository _orderRepository;

    public ConfirmPaymentCommandHandler(IPaymentService paymentService, IOrderRepository orderRepository)
    {
        _paymentService = paymentService;
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<ConfirmPaymentResult>> Handle(ConfirmPaymentCommand command, CancellationToken cancellationToken)
    {

        // Get ID PaymentIntent from client_secret (client_secret contains PI ID to "_secret")
        var clientSecret = command.PaymentIntentId;
        var paymentIntentId = clientSecret?.Split("_secret")?.FirstOrDefault();
        if (paymentIntentId == null) 
             return Errors.Order.InvalidPaymentIntentClientSecret;

        try
        {
            var paymentResult = await _paymentService.ConfirmPaymentAsync(paymentIntentId, command.PaymentMethodToken);
            
            if (paymentResult.Status == "succeeded")
            {
                await _orderRepository.MarkAsPaidAsync(paymentResult.PaymentIntentId);
            }

            return paymentResult;
        }
        catch (PaymentException ex)
        {
            return Errors.Order.FailedToConfirmPayment;
        }

        }
        
        
    }
