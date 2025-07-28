using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.ApiModels.Banners;

public class CreateBannerFormModel
{
    public string Name { get; set; } = null!;

    public List<SlideFormModel> Slides { get; set; } = new();
};

public class SlideFormModel
{
    public IFormFile Image { get; set; } = null!;
    public string? Text { get; set; }
    public string ButtonText { get; set; } = null!;

    public string ButtonUrl { get; set; } = null!;
    public Int16 SortOrder { get; set; }
}