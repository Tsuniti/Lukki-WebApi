using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Footers.Queries.GetAllFooterNames;

public class GetAllFooterNamesQueryHandler : 
    IRequestHandler<GetAllFooterNamesQuery, ErrorOr<List<string>>>
{
    
    private readonly IFooterRepository _footerRepository;


    public GetAllFooterNamesQueryHandler(IFooterRepository footerRepository)
    {
        _footerRepository = footerRepository;
    }

    public async Task<ErrorOr<List<string>>> Handle(GetAllFooterNamesQuery request, CancellationToken cancellationToken)
    {
        var footers = await _footerRepository.GetAllNamesAsync();

        if (footers is null || footers.Count == 0)
        {
            return Errors.Footer.NoNamesFound();
        }

        return footers;
    }
}