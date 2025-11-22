using NewYearPresents.App.Core;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Parser;

namespace NewYearPresents.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
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

        public MainViewModel(XlsmParser xlsmParser, DataManager dataManager)
        {
            ProductsView = new ProductsView(dataManager, new ProductViewModel());
            ParsingView = new ParsingView(xlsmParser, dataManager);

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