
namespace ChocolateWorldApp.Application.Common.Models;

public record PagedResult<T>(
    IReadOnlyList<T> Items,
    int TotalItemCount,
    int CurrentPage,
    int PageSize
    );