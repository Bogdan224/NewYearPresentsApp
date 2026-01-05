using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class ProductsBoxViewModel : ObservableObject
    {
        private readonly ProductsBox _productBox;
        public ProductViewModel Product { get; private set; }

        public ProductsBoxViewModel(ProductsBox p)
        {
            _productBox = p;
            Product = new(_productBox.Product);
        }

        public int Id
        {
            get { return _productBox.Id; }
        }

        public float Price30K
        {
            get { return _productBox.Price30K; }
            set
            {
                if (_productBox.Price30K == value)
                    return;
                _productBox.Price30K = value;
                OnPropertyChanged("Price30K");
            }
        }

        public float Price60K
        {
            get { return _productBox.Price60K; }
            set
            {
                if (_productBox.Price60K == value)
                    return;
                _productBox.Price60K = value;
                OnPropertyChanged("Price60K");
            }
        }

        public float Price100K
        {
            get { return _productBox.Price100K; }
            set
            {
                if (_productBox.Price100K == value)
                    return;
                _productBox.Price100K = value;
                OnPropertyChanged("Price100K");
            }
        }

        public float Price150K
        {
            get { return _productBox.Price150K; }
            set
            {
                if (_productBox.Price150K == value)
                    return;
                _productBox.Price150K = value;
                OnPropertyChanged("Price150K");
            }
        }

        public float TotalWeight
        {
            get { return _productBox.TotalWeight; }
            set
            {
                if (_productBox.TotalWeight == value) return;
                _productBox.TotalWeight = value;
                OnPropertyChanged("TotalWeight");
            }
        }

        public static implicit operator ProductsBox(ProductsBoxViewModel productsBox)
        {
            return productsBox._productBox;
        }
    }
}