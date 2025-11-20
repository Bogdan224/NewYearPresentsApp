using NewYearPresents.App.Core;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels
{
    public class ProductsViewModel : ObservableObject
    {
        private Product? selectedProduct;

        public ObservableCollection<Product> Products { get; set; }
        public Product? SelectedProduct { 
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

        public ProductsViewModel()
        {
            Products = new ObservableCollection<Product>();
            selectedProduct = null;
        }

        public ProductsViewModel(DataManager dataManager)
        {
            Products = new ObservableCollection<Product>(dataManager.Products.GetProductsAsync().Result.ToList());
            selectedProduct = Products.Count > 0 ? Products[0] : null;
        }
    }
}
