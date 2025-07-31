using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Footers.Queries.GetFooter;

public class GetFooterQueryHandler : 
    IRequestHandler<GetFooterQuery, ErrorOr<Footer>>
{
    
    private readonly IFooterRepository _footerRepository;


    public GetFooterQueryHandler(IFooterRepository footerRepository)
    {
        _footerRepository = footerRepository;
    }

    public async Task<ErrorOr<Footer>> Handle(GetFooterQuery query, CancellationToken cancellationToken)
    {
        
        if (await _footerRepository.GetByNameAsync(query.Name) is not Footer footer)
        {
            return Errors.Footer.NotFound(query.Name);
        }
        
        return footer;
    }
}