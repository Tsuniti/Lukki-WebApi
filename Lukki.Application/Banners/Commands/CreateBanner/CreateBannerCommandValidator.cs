using FluentValidation;

namespace Lukki.Application.Banners.Commands.CreateBanner;

public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
{
    public CreateBannerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 100);
           // .WithMessage("Name length must be between 3 and 100 characters.");
        
        RuleFor(x => x.Slide)
            .NotNull();
    //        .WithMessage("Slide is required.");
        
        
        RuleFor(x => x.Slide.Text)
            .MaximumLength(200);
          //  .WithMessage("Text length can't exceed 200 characters.");

        RuleFor(x => x.Slide.ButtonText)
            .NotEmpty()
            .MaximumLength(100);
            //.WithMessage("ButtonText length can't exceed 100 characters.");

            RuleFor(x => x.Slide.ButtonUrl)
                .NotEmpty()
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute));
//            .WithMessage("ButtonUrl must be a valid URL.");

        RuleFor(x => x.Slide.ButtonUrl)
            .NotEmpty();
        
        RuleFor(x => x.Slide.SortOrder)
            .NotEmpty();
    }
}