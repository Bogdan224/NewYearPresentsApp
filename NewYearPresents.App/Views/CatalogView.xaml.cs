using NewYearPresents.App.ViewModels;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        public CatalogView(CatalogViewModel catalogVM)
        {
            DataContext = catalogVM;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(DataContext != null && DataContext is CatalogViewModel catalog)
                ProductDataGrid = await catalog.UpdateDataGridAsync(ProductDataGrid);
        }

        private void ProductDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}