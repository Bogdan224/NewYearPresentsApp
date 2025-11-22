using NewYearPresents.Models.Entities;

namespace NewYearPresents.Domain.Repositories.Abstract
{
    public interface IProductTypesRepository
    {
        Task<IEnumerable<ProductType>> GetProductTypesAsync();

        Task<ProductType?> GetProductTypeByIdAsync(int id);

        Task SaveProductTypeAsync(ProductType entity);

        Task SaveProductTypeAsync(IEnumerable<ProductType> entities);

        Task DeleteProductTypeAsync(int id);
    }
}