using ChocolateWorldApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ChocolateWorldApp.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChocolateWorldApp.Application.Common.Interfaces;

namespace ChocolateWorldApp.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductVariant> ProductVariants { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
    }
}
