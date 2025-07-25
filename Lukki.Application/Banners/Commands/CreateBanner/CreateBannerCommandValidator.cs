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

        RuleFor(x => x.Slides)
            .NotEmpty();
        //        .WithMessage("Slide is required.");


        RuleForEach(x => x.Slides).ChildRules(slide =>
        {
            slide.RuleFor(s => s.Text)
                .MaximumLength(200);
                //.WithMessage("Text length can't exceed 200 characters.");

                slide.RuleFor(s => s.ButtonText)
                    .NotEmpty()
                    //.WithMessage("ButtonText is required.")
                    .MaximumLength(100);
                //.WithMessage("ButtonText length can't exceed 100 characters.");

                slide.RuleFor(s => s.ButtonUrl)
                    .NotEmpty()
                    //.WithMessage("ButtonUrl is required.")
                    .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute));
                //.WithMessage("ButtonUrl must be a valid URL.");
        });

    }
}