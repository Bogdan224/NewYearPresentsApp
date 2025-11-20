using NewYearPresents.App.ViewModels;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        private int shownProducts = 0;
        private const int shownProductsConst = 50;

        private DataManager _dataManager;

        internal BindingList<ProductViewModel> Products { get; set; }

        public ProductsView(DataManager dataManager, ProductViewModel productViewModel)
        {
            _dataManager = dataManager;
            Products = new BindingList<ProductViewModel>();

            InitializeComponent();

            DataContext = productViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            dataGrid.DataContext = Products;
            dataGrid.ItemsSource = Products;

            //UpdateDataGrid(shownProducts, shownProducts + shownProductsConst);
            //shownProducts += shownProductsConst;

        }

        private async void UpdateDataGrid()
        {
            if (Products.Count != 0) Products.Clear();
            foreach (var product in await _dataManager.Products.GetProductsAsync())
            {
                Products.Add(new ProductViewModel(product));
            }            
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
