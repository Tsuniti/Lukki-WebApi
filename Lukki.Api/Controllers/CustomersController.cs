using MapsterMapper;
using MediatR;

namespace Lukki.Api.Controllers;

public class CustomersController : ApiController
{

    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public CustomersController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
}