using NewYearPresents.App.ViewModels;
using System.Threading.Tasks;
using System.Windows;
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
            DataContext = storageViewModel;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await ((StorageViewModel)DataContext).AddDataGrids(ProductsBoxesDataGrid, PackagingsDataGrid);
        }

        private void ProductTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            ((StorageViewModel)DataContext).CurrentObject = StorageViewModel.CurrentObjectInStorage.ProductBox;
        }

        private void PackagingTabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            ((StorageViewModel)DataContext).CurrentObject = StorageViewModel.CurrentObjectInStorage.Packaging;
        }
    }
}
