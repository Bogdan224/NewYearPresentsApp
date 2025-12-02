using NewYearPresents.App.Core;
using NewYearPresents.App.Infrastructure;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Parser;

namespace NewYearPresents.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public Company Company { get; set; }

        public RelayCommand ProductsViewCommand { get; set; }
        public RelayCommand ParsingViewCommand { get; set; }

        public ProductsView ProductsView { get; set; }
        public ParsingView ParsingView { get; set; }

        public object? _currentView;

        public object? CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(XlsmParser xlsmParser, AppDbContext context, Company company)
        {
            Company = company;

            ProductsView = new ProductsView(context, new ProductViewModel());
            ParsingView = new ParsingView(xlsmParser, context);

            CurrentView = ProductsView;

            ProductsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ProductsView;
            });

            ParsingViewCommand = new RelayCommand(o =>
            {
                CurrentView = ParsingView;
            });
        }
    }
}