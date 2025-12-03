using NewYearPresents.App.ViewModels;
using System.Windows.Controls;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для StorageView.xaml
    /// </summary>
    public partial class StorageView : UserControl
    {
        public StorageView(StorageViewModel storageViewModel)
        {
            InitializeComponent();
            storageViewModel.AddDataGrids(ProductsBoxesDataGrid, PackagingsDataGrid);
            DataContext = storageViewModel;
        }

        private void StorageDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
    }
}
