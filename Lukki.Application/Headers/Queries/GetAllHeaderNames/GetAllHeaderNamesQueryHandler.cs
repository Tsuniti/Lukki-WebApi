using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Headers.Queries.GetAllHeaderNames;

public class GetAllHeaderNamesQueryHandler : 
    IRequestHandler<GetAllHeaderNamesQuery, ErrorOr<List<string>>>
{
    
    private readonly IHeaderRepository _headerRepository;


    public GetAllHeaderNamesQueryHandler(IHeaderRepository headerRepository)
    {
        _headerRepository = headerRepository;
    }

    public async Task<ErrorOr<List<string>>> Handle(GetAllHeaderNamesQuery request, CancellationToken cancellationToken)
    {
        var headers = await _headerRepository.GetAllNamesAsync();

        if (headers is null || headers.Count == 0)
        {
            return Errors.Header.NoNamesFound();
        }

        return headers;
    }
}