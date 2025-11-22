using Microsoft.EntityFrameworkCore;
using NewYearPresents.Domain.Repositories.Abstract;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.Domain.Repositories.EntityFramework
{
    public class EFProductTypesRepository : IProductTypesRepository
    {
        private readonly AppDbContext _context;

        public EFProductTypesRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task SaveProductTypeAsync(ProductType entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task SaveProductTypeAsync(IEnumerable<ProductType> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductTypeAsync(int id)
        {
            _context.Entry(new ProductType() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<ProductType?> GetProductTypeByIdAsync(int id)
        {
            return await _context.ProductTypes.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.Include(x => x.Products).ToListAsync();
        }
    }
}