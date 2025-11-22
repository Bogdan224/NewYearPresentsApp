using Microsoft.EntityFrameworkCore;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Domain
{
    /// <summary>
    /// База данных в виде класса
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}