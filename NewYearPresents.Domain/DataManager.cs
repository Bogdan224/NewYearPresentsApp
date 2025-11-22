using NewYearPresents.Domain.Repositories.Abstract;

namespace NewYearPresents.Domain
{
    public class DataManager
    {
        public IProductsRepository Products { get; set; }
        public IProductTypesRepository ProductTypes { get; set; }
        public IManufacturersRepository Manufacturers { get; set; }

        public DataManager(IProductsRepository productsRepository,
            IProductTypesRepository productTypesRepository,
            IManufacturersRepository manufacturersRepository)
        {
            Products = productsRepository;
            ProductTypes = productTypesRepository;
            Manufacturers = manufacturersRepository;
        }
    }
}