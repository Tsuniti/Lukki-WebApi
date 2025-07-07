using FluentValidation;
using Lukki.Domain.ProductAggregate.Enums;

namespace Lukki.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.SellerId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.TargetGroup)
            .NotEmpty()
            .Must(value => Enum.TryParse<TargetGroup>(value, true, out _))
            .WithMessage("Invalid target group specified.");
        RuleFor(x => x.Price.Amount)
            .GreaterThan(0);
        RuleFor(x => x.Price.Currency)
            .Length(3);
        RuleFor(x => x.Images).NotEmpty();
        RuleFor(x => x.CategoryIds)
            .NotEmpty();
    }
}