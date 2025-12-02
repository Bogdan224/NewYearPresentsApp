using NewYearPresents.App.ViewModels;
using NewYearPresents.Domain;
using NewYearPresents.Models.Extentions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private int shownProducts = 0;
        private const int shownProductsConst = 50;

        private AppDbContext _context;

        internal BindingList<ProductViewModel> Products { get; set; }

        public ProductsView(AppDbContext context, ProductViewModel productViewModel)
        {
            _context = context;
            Products = new BindingList<ProductViewModel>();

            InitializeComponent();

            DataContext = productViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            ProductDataGrid.DataContext = new ProductViewModel();
            ProductDataGrid.ItemsSource = Products;

            //UpdateDataGrid(shownProducts, shownProducts + shownProductsConst);
            //shownProducts += shownProductsConst;
        }

        private async void UpdateDataGrid()
        {
            if (Products.Count != 0) Products.Clear();
            foreach (var product in await _context.GetProductsBoxesAsync())
            {
                Products.Add(new ProductViewModel(product));
            }
        }

        private void ProductDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}