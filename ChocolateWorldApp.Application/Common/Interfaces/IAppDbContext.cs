using ChocolateWorldApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChocolateWorldApp.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Category> Categories { get;}
    DbSet<Product> Products { get; }
    DbSet<ProductVariant> ProductVariants { get; }
    DbSet<User> Users { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
}