using System.Security.Claims;

namespace ChocolateWorldApp.Api.Common;

public static class CurrentUser
{
    public static Guid Id(ClaimsPrincipal user)
    {
        var raw = user.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? user.FindFirst("sub")?.Value
            ?? throw new UnauthorizedAccessException("Missing user id in token");
        return Guid.Parse(raw);
    }
}