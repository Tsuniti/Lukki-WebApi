using Lukki.Domain.TextboxBannerAggregate.ValueObjects;
using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Domain.TextboxBannerAggregate;

public sealed class TextboxBanner : AggregateRoot<TextboxBannerId>
{
    
    public string Name { get; private set; }
    public string Text { get; private set; }
    public string Description { get; private set; }
    public string Placeholder { get; private set; }
    public string ButtonText { get; private set; }
    public Image Background { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private TextboxBanner(
        TextboxBannerId textboxBannerId,
        string name,
        string text,
        string description,
        string placeholder,
        string buttonText,
        Image background,
        DateTime createdAt
    ) : base(textboxBannerId)
    {
        Name = name;
        Text = text;
        Description = description;
        Placeholder = placeholder;
        ButtonText = buttonText;
        Background = background;
        CreatedAt = createdAt;
    }
    
    public static TextboxBanner Create(
        String name,
        String text,
        string description,
        string placeholder,
        string buttonText,
        Image background
        
    )
    {
        return new(
            TextboxBannerId.CreateUnique(),
            name,
            text,
            description,
            placeholder,
            buttonText,
            background,
            DateTime.UtcNow
        );
    }
    
    
#pragma warning disable CS8618
    private TextboxBanner()
    {
        // EF Core requires a parameterless constructor for materialization
    }
#pragma warning restore CS8618
}