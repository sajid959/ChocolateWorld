namespace ChocolateWorldApp.Application.Products.Queries;

public record GetProductsByCategoryQuery(string Slug, int CurrentPage,  int PageSize);