using System.Security.Claims;
using Lukki.Application.Orders.Commands.CreateOrder;
using Lukki.Contracts.Orders;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;


[Route("orders")]
public class OrdersController : ApiController
{

    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public OrdersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var command = _mapper.Map<CreateOrderCommand>(request);

        var createOrderResult = await _mediator.Send(command with { CustomerId = customerId });
        
        return createOrderResult.Match(
            order => Ok(_mapper.Map<OrderResponse>(order)),
            errors => Problem(errors) 
        );
    }
}