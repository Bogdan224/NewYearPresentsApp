using NewYearPresents.Models.Entities;

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