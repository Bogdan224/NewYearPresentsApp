using Microsoft.EntityFrameworkCore;
using NewYearPresents.Domain.Repositories.Abstract;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Domain.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public EFProductsRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task SaveProductAsync(Product entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveProductAsync(IEnumerable<Product> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            _context.Entry(new Product() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(x => x.ProductType).Include(x => x.Manufacturer).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(x => x.ProductType).Include(x => x.Manufacturer).ToListAsync();
        }
    }
}