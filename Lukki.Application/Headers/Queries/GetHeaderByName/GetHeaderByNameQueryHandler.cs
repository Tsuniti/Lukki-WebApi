using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.HeaderAggregate;
using MediatR;

namespace Lukki.Application.Headers.Queries.GetHeaderByName;

public class GetHeaderByNameQueryHandler : 
    IRequestHandler<GetHeaderByNameQuery, ErrorOr<MyHeader>>
{
    
    private readonly IHeaderRepository _headerRepository;


    public GetHeaderByNameQueryHandler(IHeaderRepository headerRepository)
    {
        _headerRepository = headerRepository;
    }

    public async Task<ErrorOr<MyHeader>> Handle(GetHeaderByNameQuery byNameQuery, CancellationToken cancellationToken)
    {
        
        if (await _headerRepository.GetByNameAsync(byNameQuery.Name) is not MyHeader header)
        {
            return Errors.Header.NotFound(byNameQuery.Name);
        }
        
        return header;
    }
}