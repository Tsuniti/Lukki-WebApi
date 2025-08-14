using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.ApiModels.Footer;

public class CreateFooterFormModel
{
    public string Name { get; set; } = null!;
    public string CopyrightText { get; set; } = null!;
    [FromForm]
    public List<FooterSectionFormModel> Sections { get; set; } = new();
};

public class FooterSectionFormModel
{
    public string Name { get; set; } = null!;
    public List<FooterLinkFormModel> Links { get; set; } = new();
    public Int16 SortOrder { get; set; }
}
public class FooterLinkFormModel
{
    public string? Text { get; set; }
    public string Url { get; set; } = null!;
    public IFormFile? Icon { get; set; }
    public Int16 SortOrder { get; set; }
}