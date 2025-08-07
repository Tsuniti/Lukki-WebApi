using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Lukki.Api.ApiModels.Banners;

public class CreateBannerFormModel
{
    public string Name { get; set; } = null!;

    public List<SlideFormModel> Slides { get; set; }
};

public class SlideFormModel
{
    public IFormFile Image { get; set; } = null!;
    public string? Text { get; set; }
    
    public string? ButtonText { get; set; }
    
    public string? ButtonUrl { get; set; }

    public short SortOrder { get; set; }
}