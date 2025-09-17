using ErrorOr;
using Lukki.Domain.PromoCategoryAggregate;
using MediatR;

namespace Lukki.Application.PromoCategories.Queries.GetAllPromoCategories;

public class GetAllPromoCategoriesQuery : IRequest<ErrorOr<List<PromoCategory>>>;