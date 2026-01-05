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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для OrderView.xaml
    /// </summary>
    public partial class PresentsView : UserControl
    {
        public PresentsView(PresentsViewModel presentsViewModel)
        {
            InitializeComponent();
            DataContext = presentsViewModel;
        }
    }
}
