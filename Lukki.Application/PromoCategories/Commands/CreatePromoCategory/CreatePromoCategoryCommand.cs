using ErrorOr;
using Lukki.Domain.PromoCategoryAggregate;
using MediatR;

namespace Lukki.Application.PromoCategories.Commands.CreatePromoCategory;

public record CreatePromoCategoryCommand( 
    string Name
    ) : IRequest<ErrorOr<PromoCategory>>;