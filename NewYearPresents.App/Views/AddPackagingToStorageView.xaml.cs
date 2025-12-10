using NewYearPresents.App.ViewModels;
using System.Threading.Tasks;
using System.Windows;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для AddPackagingToStorageView.xaml
    /// </summary>
    public partial class AddPackagingToStorageView : Window
    {
        public AddPackagingToStorageView(AddPackagingToStorageViewModel addPackagingToStorageViewModel)
        {
            InitializeComponent();
            DataContext = addPackagingToStorageViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ((AddPackagingToStorageViewModel)DataContext).InitializeAsync();
        }
    }
}
