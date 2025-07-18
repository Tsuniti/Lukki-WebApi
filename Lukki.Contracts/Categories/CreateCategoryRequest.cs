namespace Lukki.Contracts.Categories;

public record CreateCategoryRequest
(
string Name,
string? ParentCategoryId
);