using NewYearPresents.App.Core;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.App.ViewModels.Entities
{
    public class PackagingInStorageViewModel : ObservableObject
    {
        private readonly PackagingInStorage _packagingInStorage;

        public PackagingInStorageViewModel(PackagingInStorage packagingInStorage)
        {
            _packagingInStorage = packagingInStorage;
        }

        public int PackagingId
        {
            get => _packagingInStorage.PackagingId;
        }

        public int Id
        {
            get => _packagingInStorage.Id;
        }

        public string? Name
        {
            get => _packagingInStorage.Packaging.Name;
            set
            {
                if (_packagingInStorage.Packaging.Name == value) return;
                _packagingInStorage.Packaging.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public float MaxWeight
        {
            get => _packagingInStorage.Packaging.MaxWeight;
            set
            {
                if (_packagingInStorage.Packaging.MaxWeight == value) return;
                _packagingInStorage.Packaging.MaxWeight = value;
                OnPropertyChanged(nameof(MaxWeight));
            }
        }

        public int Count
        {
            get => _packagingInStorage.Count;
            set
            {
                if (_packagingInStorage.Count == value) return;
                _packagingInStorage.Count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
    }
}
