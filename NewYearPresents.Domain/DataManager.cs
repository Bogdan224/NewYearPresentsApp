using Microsoft.EntityFrameworkCore;
using NewYearPresents.Domain.Repositories.Abstract;
using NewYearPresents.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
