using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using Lukki.Domain.PromoCategoryAggregate;
using MediatR;

namespace Lukki.Application.PromoCategories.Commands.CreatePromoCategory;

public class CreatePromoCategoryCommandHandler : IRequestHandler<CreatePromoCategoryCommand, ErrorOr<PromoCategory>>
{
    
    private readonly IPromoCategoryRepository _promoCategoryRepository;

    public CreatePromoCategoryCommandHandler(IPromoCategoryRepository promoCategoryRepository)
    {
        _promoCategoryRepository = promoCategoryRepository;
    }

    public async Task<ErrorOr<PromoCategory>> Handle(CreatePromoCategoryCommand command, CancellationToken cancellationToken)
    {
        
        
        // Validate the promoCategory doesn't exist
        if (await _promoCategoryRepository.GetByName(command.Name) is not null)
        {
            return Errors.PromoCategory.DuplicateName(command.Name);
        }
        
        
        
        // Create PromoCategory
        var promoCategory = PromoCategory.Create(
            name: command.Name
        );
        // Persist PromoCategory
        await _promoCategoryRepository.AddAsync(promoCategory);
        // Return PromoCategory
        return promoCategory;

    }
}