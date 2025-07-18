namespace Lukki.Contracts.Categories;

public record CategoryResponse
(
    string Id,
    string Name,
    string? ParentCategoryId
);