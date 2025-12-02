using Microsoft.EntityFrameworkCore;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.Extentions
{
    public static class AppDbContextExtentions
    {
        //ProductsBox
        public static async Task SaveProductsBoxAsync(this AppDbContext _context, ProductsBox entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteProductsBoxAsync(this AppDbContext _context, int id)
        {
            _context.Entry(new ProductsBox() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public static async Task<ProductsBox?> GetProductsBoxByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.ProductsBoxes.Include(x=>x.Product)
                .Include(x => x.Product.ProductType)
                .Include(x => x.Product.Manufacturer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<ProductsBox>> GetProductsBoxesAsync(this AppDbContext _context)
        {
            return await _context.ProductsBoxes.Include(x => x.Product)
                .Include(x => x.Product.ProductType)
                .Include(x => x.Product.Manufacturer)
                .ToListAsync();
        }

        //Manufacturers
        public static async Task SaveManufacturerAsync(this AppDbContext _context, Manufacturer entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteManufacturerAsync(this AppDbContext _context, int id)
        {
            _context.Entry(new Manufacturer() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public static async Task<Manufacturer?> GetManufacturerByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.Manufacturers.Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<Manufacturer>> GetManufacturersAsync(this AppDbContext _context)
        {
            return await _context.Manufacturers.Include(x => x.Products)
                .ToListAsync();
        }

        //ProductType
        public static async Task SaveProductTypeAsync(this AppDbContext _context, ProductType entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteProductTypeAsync(this AppDbContext _context, int id)
        {
            _context.Entry(new ProductType() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        public static async Task<ProductType?> GetProductTypeByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.ProductTypes.Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<ProductType>> GetProductTypesAsync(this AppDbContext _context)
        {
            return await _context.ProductTypes.Include(x => x.Products)
                .ToListAsync();
        }
    }
}
