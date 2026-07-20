using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Application.Users.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Users.Queries;

public class GetMeHandler
{
    private readonly IAppDbContext _context;
    public GetMeHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<MeDto> HandleAsync(GetMeQuery query, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .Where( u => u.Id == query.UserId)
            .Select(u => new MeDto(
                u.Id,
                u.Name,
                u.Email,
                u.Phone,
                u.Role,
                u.CreatedAt
                )).SingleOrDefaultAsync(cancellationToken)
            ?? throw new KeyNotFoundException("USer not found");
    }
    
}