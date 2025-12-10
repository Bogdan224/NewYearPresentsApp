using NewYearPresents.App.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для AddProductsBoxToStorage.xaml
    /// </summary>
    public partial class AddProductsBoxToStorageView : Window
    {
        public AddProductsBoxToStorageView(AddProductsBoxToStorageViewModel addProductsBoxToStorageViewModel)
        {
            InitializeComponent();
            DataContext = addProductsBoxToStorageViewModel;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await ((AddProductsBoxToStorageViewModel)DataContext).InitializeAsync();
        }
    }
}
