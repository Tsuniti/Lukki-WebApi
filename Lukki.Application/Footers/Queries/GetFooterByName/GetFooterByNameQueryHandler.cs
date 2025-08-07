using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.FooterAggregate;
using MediatR;

namespace Lukki.Application.Footers.Queries.GetFooterByName;

public class GetFooterByNameQueryHandler : 
    IRequestHandler<GetFooterByNameQuery, ErrorOr<Footer>>
{
    
    private readonly IFooterRepository _footerRepository;


    public GetFooterByNameQueryHandler(IFooterRepository footerRepository)
    {
        _footerRepository = footerRepository;
    }

    public async Task<ErrorOr<Footer>> Handle(GetFooterByNameQuery byNameQuery, CancellationToken cancellationToken)
    {
        
        if (await _footerRepository.GetByNameAsync(byNameQuery.Name) is not Footer footer)
        {
            return Errors.Footer.NotFound(byNameQuery.Name);
        }
        
        return footer;
    }
}