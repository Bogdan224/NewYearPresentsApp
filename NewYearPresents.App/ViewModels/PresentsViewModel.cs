using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels
{
    public class PresentsViewModel : ObservableObject
    {
        private ObservableCollection<PresentsViewModel> _presents;
        private ObservableCollection<ProductInPresent> _productsInPresent;

        public ObservableCollection<PresentsViewModel> Presents
        {
            get => _presents;
            set
            {
                _presents = value;
                OnPropertyChanged(nameof(Presents));
            }
        }
        public ObservableCollection<ProductInPresent> ProductsInPresent
        {
            get => _productsInPresent;
            set
            {
                _productsInPresent = value;
                OnPropertyChanged(nameof(ProductsInPresent));
            }
        }

        public PresentsViewModel() 
        {
            _presents = new ObservableCollection<PresentsViewModel>();
            _productsInPresent = new ObservableCollection<ProductInPresent>();
        }
    }
}
