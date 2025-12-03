using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class ProductsBoxInStorageViewModel : ObservableObject
    {
        private ProductsBoxInStorage _productsBoxInStorage;

        public ProductsBoxInStorageViewModel(ProductsBoxInStorage productsBoxInStorage)
        {
            _productsBoxInStorage = productsBoxInStorage;
        }

        public int Id
        {
            get { return _productsBoxInStorage.ProductsBox.Id; }
        }

        public string? Name
        {
            get { return _productsBoxInStorage.ProductsBox.Product.Name; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Product.Name == value)
                    return;
                _productsBoxInStorage.ProductsBox.Product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string? ManufacturerName
        {
            get { return _productsBoxInStorage.ProductsBox.Product.Manufacturer?.Name; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Product.Manufacturer is null || _productsBoxInStorage.ProductsBox.Product.Manufacturer.Name == value)
                    return;
                _productsBoxInStorage.ProductsBox.Product.Manufacturer.Name = value;
                OnPropertyChanged("ManufacturerName");
            }
        }

        public string? ProductTypeName
        {
            get { return _productsBoxInStorage.ProductsBox.Product.ProductType?.Name; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Product.ProductType is null || _productsBoxInStorage.ProductsBox.Product.ProductType.Name == value)
                    return;
                _productsBoxInStorage.ProductsBox.Product.ProductType.Name = value;
                OnPropertyChanged("ProductTypeName");
            }
        }

        public float Price30K
        {
            get { return _productsBoxInStorage.ProductsBox.Price30K; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Price30K == value)
                    return;
                _productsBoxInStorage.ProductsBox.Price30K = value;
                OnPropertyChanged("Price30K");
            }
        }

        public float Price60K
        {
            get { return _productsBoxInStorage.ProductsBox.Price60K; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Price60K == value)
                    return;
                _productsBoxInStorage.ProductsBox.Price60K = value;
                OnPropertyChanged("Price60K");
            }
        }

        public float Price100K
        {
            get { return _productsBoxInStorage.ProductsBox.Price100K; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Price100K == value)
                    return;
                _productsBoxInStorage.ProductsBox.Price100K = value;
                OnPropertyChanged("Price100K");
            }
        }

        public float Price150K
        {
            get { return _productsBoxInStorage.ProductsBox.Price150K; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Price150K == value)
                    return;
                _productsBoxInStorage.ProductsBox.Price150K = value;
                OnPropertyChanged("Price150K");
            }
        }

        public float TotalWeight
        {
            get { return _productsBoxInStorage.ProductsBox.TotalWeight; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.TotalWeight == value) return;
                _productsBoxInStorage.ProductsBox.TotalWeight = value;
                OnPropertyChanged("TotalWeight");
            }
        }

        public int ExpirationDate
        {
            get { return _productsBoxInStorage.ProductsBox.Product.ExpirationDate; }
            set
            {
                if (_productsBoxInStorage.ProductsBox.Product.ExpirationDate == value)
                    return;
                _productsBoxInStorage.ProductsBox.Product.ExpirationDate = value;
                OnPropertyChanged("ExpirationDate");
            }
        }

        public int Count
        {
            get { return _productsBoxInStorage.Count; }
            set
            {
                if (_productsBoxInStorage.Count == value)
                    return;
                _productsBoxInStorage.Count = value;
                OnPropertyChanged("ExpirationDate");
            }
        }
    }
}
