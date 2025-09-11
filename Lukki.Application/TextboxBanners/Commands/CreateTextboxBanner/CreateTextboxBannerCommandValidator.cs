using FluentValidation;

namespace Lukki.Application.TextboxBanners.Commands.CreateTextboxBanner;

public class CreateTextboxBannerCommandValidator : AbstractValidator<CreateTextboxBannerCommand>
{
    public CreateTextboxBannerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(3, 100);
        // .WithMessage("Name length must be between 3 and 100 characters.");
        
        RuleFor(x => x.Text)
            .NotEmpty()
            .Length(3, 500);
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .Length(3, 1000);
        
        RuleFor(x => x.Placeholder)
            .NotEmpty()
            .Length(3, 200);
        RuleFor(x => x.ButtonText)
            .NotEmpty()
            .Length(1, 100);
        RuleFor(x => x.BackgroundImageStream)
            .NotNull()
            .Must(stream => stream.Length > 0)
            .WithMessage("Background image stream must not be empty.");
    }
}