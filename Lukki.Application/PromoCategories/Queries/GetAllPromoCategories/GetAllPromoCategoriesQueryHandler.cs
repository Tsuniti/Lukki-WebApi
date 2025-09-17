using Lukki.Domain.PromoCategoryAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.PromoCategories.Queries.GetAllPromoCategories;

public class GetAllPromoCategoriesQueryHandler :
    IRequestHandler<GetAllPromoCategoriesQuery, ErrorOr<List<PromoCategory>>>
{
    private readonly IPromoCategoryRepository _promoCategoryRepository;

    public GetAllPromoCategoriesQueryHandler(IPromoCategoryRepository promoCategoryRepository)
    {
        _promoCategoryRepository = promoCategoryRepository;
    }
    
    public async Task<ErrorOr<List<PromoCategory>>> Handle(GetAllPromoCategoriesQuery request, CancellationToken cancellationToken)
    {
        var promoCategories = await _promoCategoryRepository.GetAllAsync();

        if (promoCategories is null || promoCategories.Count == 0)
        {
            return Errors.PromoCategory.NoPromoCategoriesFound();
        }

        return promoCategories;
    }
}