using NewYearPresents.App.Core;
using NewYearPresents.App.ViewModels.Entities;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.ViewModels
{
    public class StorageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        private ObservableCollection<ProductsBoxInStorageViewModel> productsBoxesInStorage;
        private ObservableCollection<PackagingInStorageViewModel> packagingsInStorage;

        public ObservableCollection<ProductsBoxInStorageViewModel> ProductsBoxesInStorage 
        {
            get => productsBoxesInStorage;
            set
            {
                productsBoxesInStorage = value;
                OnPropertyChanged(nameof(ProductsBoxesInStorage));
            }
        }
        public ObservableCollection<PackagingInStorageViewModel> PackagingsInStorage
        {
            get => packagingsInStorage;
            set
            {
                packagingsInStorage = value;
                OnPropertyChanged(nameof(PackagingsInStorage));
            }
        }
        public object CurrentObject { get; set; }

        public enum CurrentObjectInStorage
        {
            ProductBox,
            Packaging
        }
        public CurrentObjectInStorage CurrentObjectType { get; set; }

        public ButtonCommand AddingInStorage { get; private set; }
        public ButtonCommand DeleteButtonCommand { get; private set; }

        public StorageViewModel(AppDbContext context)
        {
            _context = context;
            ProductsBoxesInStorage = new();
            PackagingsInStorage = new();

            AddingInStorage = new(async x =>
            {
                await AddingInStorageAsync();
            });

            DeleteButtonCommand = new(async x => await DeletingFromStorageAsync());
        }

        public async Task UpdateStorageContentAsync()
        {
            await UpdateProductsBoxesInStorageAsync();
            await UpdatePackagingsInStorageAsync();
        }

        private async Task UpdateProductsBoxesInStorageAsync()
        {
            try
            {
                var products = await _context.GetProductsBoxesInStorageAsync();
                ProductsBoxesInStorage = new ObservableCollection<ProductsBoxInStorageViewModel>(products.Select(x=>new ProductsBoxInStorageViewModel(x)));
            }
            catch (Exception e)
            {
                MessageBox.Show(nameof(this.UpdateProductsBoxesInStorageAsync) + "\n" + e.Message);
            }
        }
        private async Task UpdatePackagingsInStorageAsync()
        {
            try
            {
                var packagings = await _context.GetPackagingsInStorageAsync();
                PackagingsInStorage = new ObservableCollection<PackagingInStorageViewModel>(packagings.Select(x=>new PackagingInStorageViewModel(x)));
            }
            catch (Exception e)
            {
                MessageBox.Show(nameof(this.UpdatePackagingsInStorageAsync) + "\n" + e.Message);
            }
        }

        private async Task DeletingFromStorageAsync()
        {
            try
            {
                switch (CurrentObjectType)
                {
                    case CurrentObjectInStorage.ProductBox:
                        await _context.DeleteProductsBoxInStorageAsync(((ProductsBoxInStorageViewModel)CurrentObject).Id);
                        await UpdateProductsBoxesInStorageAsync();
                        break;
                    case CurrentObjectInStorage.Packaging:
                        await _context.DeletePackagingInStorageAsync(((PackagingInStorageViewModel)CurrentObject).Id);
                        await UpdatePackagingsInStorageAsync();
                        break;
                    default:
                        throw new Exception();
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить объект");
            }
        }

        private async Task AddingInStorageAsync()
        {
            switch (CurrentObjectType)
            {
                case CurrentObjectInStorage.ProductBox:
                    AddProductsBoxToStorageView productsView = new(new AddProductsBoxToStorageViewModel(_context));
                    productsView.ShowDialog();
                    await UpdateProductsBoxesInStorageAsync();
                    break;
                case CurrentObjectInStorage.Packaging:
                    AddPackagingToStorageView packagingView = new(new AddPackagingToStorageViewModel(_context));
                    packagingView.ShowDialog();
                    await UpdatePackagingsInStorageAsync();
                    break;
            }
        }

    }
}
