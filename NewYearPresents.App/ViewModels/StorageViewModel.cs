using NewYearPresents.App.Core;
using NewYearPresents.App.ViewModels.Entities;
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

        private enum CurrentObjectInStorage
        {
            ProductBox,
            Packaging
        }
        private CurrentObjectInStorage currentObject;

        public RelayCommand ProductsBoxesDataGridRelay { get; private set; }
        public RelayCommand PackagingsDataGridRelay { get; private set; }

        public DataGrid? ProductsBoxesDataGrid { get; private set; }
        public DataGrid? PackagingsDataGrid { get; private set; }

        public StorageViewModel(AppDbContext context)
        {
            _context = context;
            ProductsBoxesInStorage = new();
            PackagingsInStorage = new();
            currentObject = CurrentObjectInStorage.ProductBox;

            ProductsBoxesDataGridRelay = new(async ex =>
            {
                if (ProductsBoxesDataGrid == null || PackagingsDataGrid == null || currentObject == CurrentObjectInStorage.ProductBox) return;
                (ProductsBoxesDataGrid.Visibility, PackagingsDataGrid.Visibility) = (PackagingsDataGrid.Visibility, ProductsBoxesDataGrid.Visibility);

                await UpdateProductsBoxesInStorageAsync();
                ProductsBoxesDataGrid.ItemsSource = ProductsBoxesInStorage;

                currentObject = CurrentObjectInStorage.ProductBox;
            });

            PackagingsDataGridRelay = new(async ex =>
            {
                if (ProductsBoxesDataGrid == null || PackagingsDataGrid == null || currentObject == CurrentObjectInStorage.Packaging) return;
                (ProductsBoxesDataGrid.Visibility, PackagingsDataGrid.Visibility) = (PackagingsDataGrid.Visibility, ProductsBoxesDataGrid.Visibility);

                await UpdatePackagingsInStorageAsync();
                PackagingsDataGrid.ItemsSource = PackagingsInStorage;

                currentObject = CurrentObjectInStorage.Packaging;
            }); 
        }

        public void AddDataGrids(DataGrid productsDataGrid, DataGrid packagingsDataGrid)
        {
            ProductsBoxesDataGrid = productsDataGrid;
            ProductsBoxesDataGrid.DataContext = new ProductsBoxInStorageViewModel(new());

            PackagingsDataGrid = packagingsDataGrid;
            PackagingsDataGrid.DataContext = new PackagingInStorageViewModel(new());

            ProductsBoxesDataGridRelay.Execute(this);
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}
