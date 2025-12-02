using Microsoft.EntityFrameworkCore;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Domain
{
    /// <summary>
    /// База данных в виде класса
    /// </summary>
    public class AppDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Packaging> Packagings { get; set; }
        public DbSet<PackagingInStorage> PackagingsInStorage { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<PresentInOrder> PresentsInOrder { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductInPresent> ProductsInPresent { get; set; }
        public DbSet<ProductsBox> ProductsBoxes { get; set; }
        public DbSet<ProductsBoxInOrder> ProductsBoxesInOrder { get; set; }
        public DbSet<ProductsBoxInStorage> ProductsBoxesInStorage { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}