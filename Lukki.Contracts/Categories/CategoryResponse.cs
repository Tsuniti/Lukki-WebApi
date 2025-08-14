namespace Lukki.Contracts.Categories;

public record CategoryResponse
(
    string Id,
    string Name,
    List<CategoryResponse> SubCategories
    );