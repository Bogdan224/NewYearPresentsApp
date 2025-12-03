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
        private readonly PackagingInStorage packagingInStorage;

        public PackagingInStorageViewModel(PackagingInStorage packagingInStorage)
        {
            this.packagingInStorage = packagingInStorage;
        }

        public int Id
        {
            get { return packagingInStorage.Id; }
        }

        public string? Name
        {
            get { return packagingInStorage.Packaging.Name; }
            set
            {
                if (packagingInStorage.Packaging.Name == value) return;
                packagingInStorage.Packaging.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public float MaxWeight
        {
            get { return packagingInStorage.Packaging.MaxWeight; }
            set
            {
                if (packagingInStorage.Packaging.MaxWeight == value) return;
                packagingInStorage.Packaging.MaxWeight = value;
                OnPropertyChanged(nameof(MaxWeight));
            }
        }

        public int Count
        {
            get { return packagingInStorage.Count; }
            set
            {
                if (packagingInStorage.Count == value) return;
                packagingInStorage.Count = value;
                OnPropertyChanged(nameof(Count));
            }
        }
    }
}
