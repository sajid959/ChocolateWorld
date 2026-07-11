namespace ChocolateWorldApp.Application.Products.Queries;

public record SearchProductQuery(string SearchText, int Page, int PageSize);