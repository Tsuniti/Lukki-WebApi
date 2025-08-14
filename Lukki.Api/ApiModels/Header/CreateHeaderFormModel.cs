namespace Lukki.Contracts.Header;

public class CreateHeaderFormModel
{
    public string Name { get; set; }
    public List<HeaderBurgerMenuLinkFormModel> BurgerMenuLinks { get; set; }
    public IFormFile Logo { get; set; }
    public IFormFile OnHoverLogo { get; set; }
    public List<HeaderIconButtonFormModel> Buttons { get; set; }
};

public class HeaderBurgerMenuLinkFormModel
{
    public string Text { get; set; }
    public string Url { get; set; }
    public Int16 SortOrder { get; set; }
};

public class HeaderIconButtonFormModel
{
    public IFormFile Icon { get; set; }
    public string Url { get; set; }
    public Int16 SortOrder { get; set; }
};