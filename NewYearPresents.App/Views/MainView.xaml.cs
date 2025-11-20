using NewYearPresents.App.ViewModels;
using System.Windows;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView(MainViewModel mainView)
        {
            InitializeComponent();
            DataContext = mainView;
        }
    }
}
