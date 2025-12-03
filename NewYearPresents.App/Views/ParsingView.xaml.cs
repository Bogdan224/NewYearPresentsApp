using NewYearPresents.App.ViewModels;
using NewYearPresents.Domain;
using NewYearPresents.Models.DTOs;
using NewYearPresents.Models.Entities;
using NewYearPresents.Parser;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.Views
{
    /// <summary>
    /// Логика взаимодействия для ParsingView.xaml
    /// </summary>
    public partial class ParsingView : UserControl
    {
        public ParsingView(ParsingViewModel parsingViewModel)
        {
            parsingViewModel.Filename = "Прайс-лист 13.10.2025г. (1) (1).xlsm";
            DataContext = parsingViewModel;
            InitializeComponent();
        }
    }
}