using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class ProductsInPresentViewModel : ObservableObject
    {
        private readonly ProductInPresent _productInPresent;

        public ProductsInPresentViewModel(ProductInPresent productInPresent)
        {
            _productInPresent = productInPresent;
            Product = new ProductViewModel(_productInPresent.Product);
        }
        public ProductViewModel Product { get; private set; }

        public int Id 
        { 
            get => _productInPresent.Id; 
        }

        public int Count
        {
            get => _productInPresent.Count;
            set
            {
                if (_productInPresent.Count == value) return;
                _productInPresent.Count = value;
                OnPropertyChanged();
            }
        }
    }
}
