using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.App.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        private readonly ProductsBox _productBox;

        public ProductViewModel(ProductsBox p)
        {
            _productBox = p;
        }

        public ProductViewModel()
        {
            _productBox = new ProductsBox();
        }

        public int Id
        {
            get { return _productBox.Id; }
        }

        public string? Name
        {
            get { return _productBox.Product.Name; }
            set
            {
                if (_productBox.Product.Name == value)
                    return;
                _productBox.Product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string? ManufacturerName
        {
            get { return _productBox.Product.Manufacturer?.Name; }
            set
            {
                if (_productBox.Product.Manufacturer is null || _productBox.Product.Manufacturer.Name == value)
                    return;
                _productBox.Product.Manufacturer.Name = value;
                OnPropertyChanged("ManufacturerName");
            }
        }

        public string? ProductTypeName
        {
            get { return _productBox.Product.ProductType?.Name; }
            set
            {
                if (_productBox.Product.ProductType is null || _productBox.Product.ProductType.Name == value)
                    return;
                _productBox.Product.ProductType.Name = value;
                OnPropertyChanged("ProductTypeName");
            }
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
        }

        public int ExpirationDate
        {
            get { return _productBox.Product.ExpirationDate; }
            set
            {
                if (_productBox.Product.ExpirationDate == value)
                    return;
                _productBox.Product.ExpirationDate = value;
                OnPropertyChanged("ExpirationDate");
            }
        }
    }
}