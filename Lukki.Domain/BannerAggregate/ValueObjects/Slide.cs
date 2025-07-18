using Lukki.Domain.Common.Models;
using Lukki.Domain.Common.ValueObjects;
using Lukki.Domain.OrderAggregate.ValueObjects;

namespace Lukki.Domain.BannerAggregate.ValueObjects;

public class Slide : ValueObject
{
    public Image Image { get; private set; }
    public string? Text { get; private set; }
    public string ButtonText { get; private set; }
    public string ButtonUrl { get; private set; }
    
    private Slide(Image image, string text, string buttonText, string buttonUrl)
    {
        Image = image;
        Text = text;
        ButtonText = buttonText;
        ButtonUrl = buttonUrl;
    }
    
    
    public static Slide Create(
        Image image, string text, string buttonText, string buttonUrl)
    {
        return new Slide(
            image, text, buttonText, buttonUrl);
    }
    
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Image;
        yield return Text;
        yield return ButtonText;
        yield return ButtonUrl;
    }
}