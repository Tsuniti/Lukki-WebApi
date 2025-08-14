using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.HeaderAggregate.ValueObjects;

namespace Lukki.Domain.HeaderAggregate;

public sealed class MyHeader  : AggregateRoot<HeaderId>
{
    private readonly List<HeaderIconButton> _buttons = new();
    private readonly List<BurgerMenuLink> _burgerMenuLinks = new();
    
    public string Name { get; set; }
    public Image Logo { get; set; }
    public Image OnHoverLogo { get; set; }
    public IReadOnlyList<BurgerMenuLink> BurgerMenuLinks => _burgerMenuLinks.AsReadOnly();
    
    public IReadOnlyList<HeaderIconButton> Buttons => _buttons.AsReadOnly();
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; private set; }

    private MyHeader(
        HeaderId headerId,
        string name,
        Image logo,
        Image onHoverLogo,
        List<BurgerMenuLink> burgerMenuLinks,
        List<HeaderIconButton> buttons,
        DateTime createdAt
    ) : base(headerId)
    {
        Name = name;
        Logo = logo;
        OnHoverLogo = onHoverLogo;
        _burgerMenuLinks = burgerMenuLinks;
        _buttons = buttons;
        CreatedAt = createdAt;
    }
    
    public static MyHeader Create(
        string name,
        Image logo,
        Image onHoverLogo,
        List<BurgerMenuLink> burgerMenuLinks,
        List<HeaderIconButton> buttons
    )
    {
        return new(
            HeaderId.CreateUnique(),
            name,
            logo,
            onHoverLogo,
            burgerMenuLinks,
            buttons,
            DateTime.UtcNow
        );
    }
    
    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MyHeader()
    {
        // EF Core requires a parameterless constructor for materialization
    }
    #pragma warning restore CS8618
}