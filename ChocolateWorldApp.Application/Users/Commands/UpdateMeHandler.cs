using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using ChocolateWorldApp.Application.Common.Interfaces;
using ChocolateWorldApp.Application.Users.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Users.Commands;

public class UpdateMeHandler
{
    private readonly IAppDbContext _context;
    private static readonly Regex EmailRegex = new(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
    public UpdateMeHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<MeDto> HandleAsync(UpdateCommand command, CancellationToken cancellationToken)
    {
        var name = (command.Name ?? "").Trim();
        var email = string.IsNullOrWhiteSpace(command.Email) ? null : command.Email.Trim();
        if (name.Length is < 1 or > 100)
        {
            throw new ArgumentException("Name must be 1-100 characters");
        }

        if (email is not null && !EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("Invalid email format");
            
        }
        
        var user = await _context.Users
            .FirstOrDefaultAsync( u => u.Id == command.UserId, cancellationToken)
            ?? throw new KeyNotFoundException("User Not Found");

        user.UpdateProfile(name, email);
        await _context.SaveChangesAsync(cancellationToken);
        return new MeDto(
            user.Id,
            user.Name,
            user.Email,
            user.Phone,
            user.Role,
            user.CreatedAt
        );
    }
}