using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Domain.Repositories.Abstract
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task SaveProductAsync(Product entity);
        Task SaveProductAsync(IEnumerable<Product> entities);
        Task DeleteProductAsync(int id);
    }
}
