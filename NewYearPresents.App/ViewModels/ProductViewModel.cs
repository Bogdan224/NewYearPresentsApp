using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;

namespace NewYearPresents.App.ViewModels
{
    public class ProductViewModel : ObservableObject
    {
        private readonly Product _product;

        public ProductViewModel(Product p)
        {
            _product = p;
        }

        public ProductViewModel()
        {
            _product = new Product();
        }

        public int Id
        {
            get { return _product.Id; }
        }

        public string? Name
        {
            get { return _product.Name; }
            set
            {
                if (_product.Name == value)
                    return;
                _product.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string? ManufacturerName
        {
            get { return _product.Manufacturer?.Name; }
            set
            {
                if (_product.Manufacturer is null || _product.Manufacturer.Name == value)
                    return;
                _product.Manufacturer.Name = value;
                OnPropertyChanged("ManufacturerName");
            }
        }

        public string? ProductTypeName
        {
            get { return _product.ProductType?.Name; }
            set
            {
                if (_product.ProductType is null || _product.ProductType.Name == value)
                    return;
                _product.ProductType.Name = value;
                OnPropertyChanged("ProductTypeName");
            }
        }

        public float Price30K
        {
            get { return _product.Price30K; }
            set
            {
                if (_product.Price30K == value)
                    return;
                _product.Price30K = value;
                OnPropertyChanged("Price30K");
            }
        }

        public float Price60K
        {
            get { return _product.Price60K; }
            set
            {
                if (_product.Price60K == value)
                    return;
                _product.Price60K = value;
                OnPropertyChanged("Price60K");
            }
        }

        public float Price100K
        {
            get { return _product.Price100K; }
            set
            {
                if (_product.Price100K == value)
                    return;
                _product.Price100K = value;
                OnPropertyChanged("Price100K");
            }
        }

        public float Price150K
        {
            get { return _product.Price150K; }
            set
            {
                if (_product.Price150K == value)
                    return;
                _product.Price150K = value;
                OnPropertyChanged("Price150K");
            }
        }

        public float TotalWeight
        {
            get { return _product.Weight * _product.Pieces; }
        }

        public int ExpirationDate
        {
            get { return _product.ExpirationDate; }
            set
            {
                if (_product.ExpirationDate == value)
                    return;
                _product.ExpirationDate = value;
                OnPropertyChanged("ExpirationDate");
            }
        }
    }
}