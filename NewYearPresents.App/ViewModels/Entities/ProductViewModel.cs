using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class ProductViewModel(Product product) : ObservableObject
    {
        private readonly Product product = product;

        public string? Name
        {
            get => product.Name;
        }

        public string? ManufacturerName
        {
            get { return product.Manufacturer?.Name; }
            set
            {
                if (product.Manufacturer is null || product.Manufacturer.Name == value)
                    return;
                product.Manufacturer.Name = value;
                OnPropertyChanged("ManufacturerName");
            }
        }

        public string? TypeName
        {
            get { return product.ProductType?.Name; }
            set
            {
                if (product.ProductType is null || product.ProductType.Name == value)
                    return;
                product.ProductType.Name = value;
                OnPropertyChanged("ProductTypeName");
            }
        }

        public float UnitWeight
        {
            get { return product.Weight; }
            set
            {
                if (product.Weight == value) return;
                product.Weight = value;
                OnPropertyChanged("TotalWeight");
            }
        }

        public int ExpirationDate
        {
            get { return product.ExpirationDate; }
            set
            {
                if (product.ExpirationDate == value)
                    return;
                product.ExpirationDate = value;
                OnPropertyChanged("ExpirationDate");
            }
        }

        public string? ImageName
        {
            get => product.ImageName;
            set
            {
                if (product.ImageName == value)
                    return;
                product.ImageName = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }
    }
}
