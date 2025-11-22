using NewYearPresents.Models.Entities;

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