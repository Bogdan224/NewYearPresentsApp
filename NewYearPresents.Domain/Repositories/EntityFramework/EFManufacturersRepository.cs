using Microsoft.EntityFrameworkCore;
using NewYearPresents.Models.Entities;
using NewYearPresents.Domain.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Domain.Repositories.EntityFramework
{
    public class EFManufacturersRepository : IManufacturersRepository
    {
        private readonly AppDbContext _context;

        public EFManufacturersRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task SaveManufacturerAsync(IEnumerable<Manufacturer> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteManufacturerAsync(int id)
        {
            _context.Entry(new Manufacturer() { Id = id }).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Manufacturer?> GetManufacturerByIdAsync(int id)
        {
            return await _context.Manufacturers.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await _context.Manufacturers.Include(x => x.Products).ToListAsync();
        }

        public async Task SaveManufacturerAsync(Manufacturer entity)
        {
            _context.Entry(entity).State = entity.Id == 0 ? EntityState.Added : EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
