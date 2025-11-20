using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Domain.Repositories.Abstract
{
    public interface IManufacturersRepository
    {
        Task<IEnumerable<Manufacturer>> GetManufacturersAsync();
        Task<Manufacturer?> GetManufacturerByIdAsync(int id);
        Task SaveManufacturerAsync(Manufacturer entity);
        Task SaveManufacturerAsync(IEnumerable<Manufacturer> entities);
        Task DeleteManufacturerAsync(int id);
    }
}
