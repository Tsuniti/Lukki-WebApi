using Lukki.Contracts.Categories;

namespace Lukki.Contracts.Header;

public record HeaderResponse(
    string Id,
    string Name,
    string LogoUrl,
    string OnHoverLogoUrl,
    BurgerMenuResponse BurgerMenu,
    List<IconButtonResponse> Buttons,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record IconButtonResponse(
    string IconUrl,
    string ButtonUrl,
    Int16 SortOrder
);

public record BurgerMenuResponse(
//    List<string> TargetGroups,
    List<CategoryResponse> Categories,
    List<LinkResponse> Links
);

public record LinkResponse(
    string Text,
    string Url,
    Int16 SortOrder
);
