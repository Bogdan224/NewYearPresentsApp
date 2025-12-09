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
    public class StorageViewModel
    {
        private readonly AppDbContext _context;

        public ObservableCollection<ProductsBoxInStorageViewModel> ProductsBoxesInStorage { get; set; }
        public ObservableCollection<PackagingInStorageViewModel> PackagingsInStorage { get; set; }

        public object CurrentObject { get; set; }

        public enum CurrentObjectInStorage
        {
            ProductBox,
            Packaging
        }
        public CurrentObjectInStorage CurrentObjectType { get; set; }

        public ButtonCommand AddingInStorage { get; private set; }
        public ButtonCommand DeleteButtonCommand { get; private set; }

        public DataGrid? ProductsBoxesDataGrid { get; private set; }
        public DataGrid? PackagingsDataGrid { get; private set; }

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

        public async Task InitializeAsync(DataGrid productsDataGrid, DataGrid packagingsDataGrid)
        {
            await UpdateStorageContentAsync();

            ProductsBoxesDataGrid = productsDataGrid;
            ProductsBoxesDataGrid.DataContext = new ProductsBoxInStorageViewModel(new());
            ProductsBoxesDataGrid.ItemsSource = ProductsBoxesInStorage;

            PackagingsDataGrid = packagingsDataGrid;
            PackagingsDataGrid.DataContext = new PackagingInStorageViewModel(new());
            PackagingsDataGrid.ItemsSource = PackagingsInStorage;
        }

        private async Task UpdateStorageContentAsync()
        {
            await UpdateProductsBoxesInStorageAsync();
            await UpdatePackagingsInStorageAsync();
        }

        private async Task UpdateProductsBoxesInStorageAsync()
        {
            try
            {
                var products = await _context.GetProductsBoxesInStorageAsync();
                ProductsBoxesInStorage = new ObservableCollection<ProductsBoxInStorageViewModel>();
                foreach (var product in products)
                {
                    ProductsBoxesInStorage.Add(new ProductsBoxInStorageViewModel(product));
                }
                if (ProductsBoxesDataGrid != null)
                    ProductsBoxesDataGrid.ItemsSource = ProductsBoxesInStorage;
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
                PackagingsInStorage = new ObservableCollection<PackagingInStorageViewModel>();
                foreach (var packaging in packagings)
                {
                    PackagingsInStorage.Add(new PackagingInStorageViewModel(packaging));
                }
                if (PackagingsDataGrid != null)
                    PackagingsDataGrid.ItemsSource = PackagingsInStorage;
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
