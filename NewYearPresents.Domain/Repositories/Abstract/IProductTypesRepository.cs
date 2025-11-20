using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
