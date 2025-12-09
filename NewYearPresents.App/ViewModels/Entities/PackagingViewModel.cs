using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class PackagingViewModel : ObservableObject
    {
        private readonly Packaging _packaging;

        public PackagingViewModel(Packaging packaging)
        {
            _packaging = packaging;
        }

        public int Id
        {
            get => _packaging.Id;
            set
            {
                if(_packaging.Id == value) return;
                _packaging.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string? Name
        {
            get => _packaging.Name;
            set
            {
                if( _packaging.Name == value) return;
                _packaging.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public float MaxWeight
        {
            get => _packaging.MaxWeight;
            set
            {
                if( _packaging.MaxWeight == value) return;
                _packaging.MaxWeight = value;
                OnPropertyChanged(nameof(MaxWeight));
            }
        }

        public static implicit operator Packaging(PackagingViewModel packaging)
        {
            return packaging._packaging;
        }

    }
}
