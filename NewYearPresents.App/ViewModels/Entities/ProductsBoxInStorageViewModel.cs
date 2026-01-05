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
        private readonly ProductsBoxInStorage _productsBoxInStorage;
        public ProductsBoxViewModel ProductsBox { get; private set; }

        public ProductsBoxInStorageViewModel(ProductsBoxInStorage productsBoxInStorage)
        {
            _productsBoxInStorage = productsBoxInStorage;
            ProductsBox = new(_productsBoxInStorage.ProductsBox);
        }

        public int Id
        {
            get { return _productsBoxInStorage.Id; }
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
