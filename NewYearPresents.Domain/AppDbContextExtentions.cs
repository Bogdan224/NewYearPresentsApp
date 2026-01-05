using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.Extentions
{
    public static class AppDbContextExtentions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString)
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
            return services;
        }

        public static async Task TruncateTableAsync<T>(
        this DbContext context,
        string schema = "dbo",
        bool resetIdentity = true)
        where T : class
        {
            var entityType = context.Model.FindEntityType(typeof(T));
            var tableName = entityType?.GetTableName();

            if (resetIdentity)
            {
                await context.Database.ExecuteSqlRawAsync(
                    $"TRUNCATE TABLE [{schema}].[{tableName}]");
            }
            else
            {
                await context.Database.ExecuteSqlRawAsync(
                    $"DELETE FROM [{schema}].[{tableName}]");
            }
        }


        //Products
        public static async Task SaveProductAsync(this AppDbContext _context, Product entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteProductAsync(this AppDbContext _context, int id)
        {
            var entity = await _context.GetProductByIdAsync(id);
            if (entity == null) return;
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<Product?> GetProductByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.Products.Include(x => x.ProductType)
                .Include(x => x.Manufacturer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<Product>> GetProductsAsync(this AppDbContext _context)
        {
            return await _context.Products.Include(x => x.ProductType)
                .Include(x => x.Manufacturer)
                .ToListAsync();
        }

        //ProductsBox
        public static async Task SaveProductsBoxAsync(this AppDbContext _context, ProductsBox entity)
        {
            if (entity.Id == 0)
            {
                if (!_context.Products.Contains(entity.Product))
                    await _context.SaveProductAsync(entity.Product);
                _context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteProductsBoxAsync(this AppDbContext _context, int id)
        {
            var entity = await _context.GetProductsBoxByIdAsync(id);
            if (entity == null) return;
            _context.ProductsBoxes.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<ProductsBox?> GetProductsBoxByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.ProductsBoxes.Include(x => x.Product)
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
            var entity = await _context.GetManufacturerByIdAsync(id);
            if (entity == null) return;
            _context.Manufacturers.Remove(entity);
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
            var entity = await _context.GetProductTypeByIdAsync(id);
            if (entity == null) return;
            _context.ProductTypes.Remove(entity);
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

        //Packaging
        public static async Task SavePackagingAsync(this AppDbContext _context, Packaging entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public static async Task DeletePackagingAsync(this AppDbContext _context, int id)
        {
            var entity = await _context.GetPackagingByIdAsync(id);
            if (entity == null) return;
            _context.Packagings.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<Packaging?> GetPackagingByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.Packagings.FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<Packaging>> GetPackagingsAsync(this AppDbContext _context)
        {
            return await _context.Packagings.ToListAsync();
        }

        //ProductsBoxInStorage
        public static async Task SaveProductsBoxInStorageAsync(this AppDbContext _context, ProductsBoxInStorage entity)
        {
            if (entity.Id == 0)
            {
                if(!_context.ProductsBoxes.Contains(entity.ProductsBox))
                    await _context.SaveProductsBoxAsync(entity.ProductsBox);
                _context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
        public static async Task DeleteProductsBoxInStorageAsync(this AppDbContext _context, int id)
        {
            var entity = await _context.GetProductsBoxInStorageByIdAsync(id);
            if (entity == null) return;
            _context.ProductsBoxesInStorage.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<ProductsBoxInStorage?> GetProductsBoxInStorageByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.ProductsBoxesInStorage.Include(x => x.ProductsBox)
                .Include(x => x.ProductsBox.Product)
                .Include(x => x.ProductsBox.Product.ProductType)
                .Include(x => x.ProductsBox.Product.Manufacturer)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<ProductsBoxInStorage>> GetProductsBoxesInStorageAsync(this AppDbContext _context)
        {
            return await _context.ProductsBoxesInStorage.Include(x => x.ProductsBox)
                .Include(x => x.ProductsBox.Product)
                .Include(x => x.ProductsBox.Product.ProductType)
                .Include(x => x.ProductsBox.Product.Manufacturer)
                .OrderBy(x => x.Id)
                .ToListAsync();
        }

        //PackagingInStorage
        public static async Task SavePackagingInStorageAsync(this AppDbContext _context, PackagingInStorage entity)
        {
            if (entity.Id == 0)
            {
                if (!_context.Packagings.Contains(entity.Packaging))
                    await _context.SavePackagingAsync(entity.Packaging);
                _context.Entry(entity).State = EntityState.Added;
            }
            else {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }
        public static async Task DeletePackagingInStorageAsync(this AppDbContext _context, int id)
        {
            var entity = await _context.GetPackagingInStorageByIdAsync(id);
            if (entity == null) return;
            _context.PackagingsInStorage.Remove(entity);
            await _context.SaveChangesAsync();
        }
        public static async Task<PackagingInStorage?> GetPackagingInStorageByIdAsync(this AppDbContext _context, int id)
        {
            return await _context.PackagingsInStorage.Include(x => x.Packaging)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public static async Task<IEnumerable<PackagingInStorage>> GetPackagingsInStorageAsync(this AppDbContext _context)
        {
            return await _context.PackagingsInStorage.Include(x => x.Packaging)
                .ToListAsync();
        }
    }
}
