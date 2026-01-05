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
        public PackagingViewModel Packaging { get; private set; }

        public PackagingInStorageViewModel(PackagingInStorage packagingInStorage)
        {
            _packagingInStorage = packagingInStorage;
            Packaging = new(_packagingInStorage.Packaging);
        }

        public int Id
        {
            get => _packagingInStorage.Id;
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
