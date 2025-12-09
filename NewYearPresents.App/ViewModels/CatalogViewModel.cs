using NewYearPresents.App.ViewModels.Entities;
using NewYearPresents.Domain;
using NewYearPresents.Models.Extentions;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.ViewModels
{
    public class CatalogViewModel
    {
        private readonly AppDbContext _context;

        public ObservableCollection<ProductsBoxViewModel> ProductsBoxes { get; private set; }

        public CatalogViewModel(AppDbContext context)
        {
            _context = context;
            ProductsBoxes = new ObservableCollection<ProductsBoxViewModel>();
        }

        private async Task UpdateCatalogContentAsync()
        {
            try
            {
                var products = await _context.GetProductsBoxesAsync();
                ProductsBoxes = new ObservableCollection<ProductsBoxViewModel>();
                foreach (var product in products)
                {
                    ProductsBoxes.Add(new ProductsBoxViewModel(product));
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public async Task<DataGrid> UpdateDataGridAsync(DataGrid dataGrid)
        {
            await UpdateCatalogContentAsync();

            dataGrid.DataContext = new ProductsBoxViewModel((Models.Entities.ProductsBox)new());
            dataGrid.ItemsSource = ProductsBoxes;

            return dataGrid;
        }

    }
}
