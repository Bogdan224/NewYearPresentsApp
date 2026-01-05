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
        public PackagingViewModel? Packaging { get; private set; }

        public PresentViewModel(Present present)
        {
            _present = present;
            if (_present.Packaging is not null) Packaging = new(_present.Packaging);
        }

        public int Id
        {
            get => _present.Id;
        }

        public string? Name
        {
            get => _present.Name;
            set
            {
                if (_present.Name == value) return;
                _present.Name = value;
                OnPropertyChanged(nameof(_present.Name));
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
