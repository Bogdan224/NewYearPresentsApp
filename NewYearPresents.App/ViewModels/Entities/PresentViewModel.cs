using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels.Entities
{
    internal class PresentViewModel : ObservableObject
    {
        private readonly Present _present;

        public PresentViewModel(Present present)
        {
            _present = present;
        }

        public int Id
        {
            get => _present.Id;
        }

        public string? PackagingName
        {
            get => _present.Packaging?.Name;
            set
            {
                if (_present.Packaging == null || _present.Packaging.Name == value) return;
                _present.Packaging.Name = value;
                OnPropertyChanged(nameof(_present.Packaging.Name));
            }
        }

        public string? PackagingImage
        {
            get => _present.Packaging?.Image;
            set
            {
                if (_present.Packaging == null || _present.Packaging.Image == value) return;
                _present.Packaging.Image = value;
                OnPropertyChanged(nameof(_present.Packaging.Image));
            }
        }

        public float TotalWeight
        {
            get => _present.TotalWeight;
            set
            {
                if (_present.TotalWeight == value) return;
                _present.TotalWeight = value;
                OnPropertyChanged(nameof(_present.TotalWeight));
            }
        }

        public float TotalPrice
        {
            get => _present.TotalPrice;
            set
            {
                if (_present.TotalPrice == value) return;
                _present.TotalPrice = value;
                OnPropertyChanged(nameof(_present.TotalPrice));
            }
        }
    }
}
