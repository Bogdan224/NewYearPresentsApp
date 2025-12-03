using NewYearPresents.App.Core;
using NewYearPresents.App.Infrastructure;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Parser;

namespace NewYearPresents.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public Company Company { get; private set; }

        public RelayCommand CatalogViewCommand { get; private set; }
        public RelayCommand ParsingViewCommand { get; private set; }
        public RelayCommand StorageViewCommand { get; private set; }

        public CatalogView CatalogView { get; private set; }
        public ParsingView ParsingView { get; private set; }
        public StorageView StorageView { get; private set; }

        public object? _currentView;

        public object? CurrentView
        {
            get { return _currentView; }
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(Company company, CatalogViewModel catalogViewModel, StorageViewModel storageViewModel, ParsingViewModel parsingViewModel)
        {
            Company = company;

            CatalogView = new CatalogView(catalogViewModel);
            StorageView = new StorageView(storageViewModel);
            ParsingView = new ParsingView(parsingViewModel);


            CurrentView = CatalogView;

            CatalogViewCommand = new RelayCommand(o =>
            {
                CurrentView = CatalogView;
            });

            StorageViewCommand = new RelayCommand(o =>
            {
                CurrentView = StorageView;
            });

            ParsingViewCommand = new RelayCommand(o =>
            {
                CurrentView = ParsingView;
            });
        }
    }
}