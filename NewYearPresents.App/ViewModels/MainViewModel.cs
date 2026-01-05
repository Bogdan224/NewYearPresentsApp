using NewYearPresents.App.Core;
using NewYearPresents.Models.Infrastructure;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Parser;
using System.Windows;
using System.Windows.Controls;

namespace NewYearPresents.App.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public Company Company { get; private set; }

        public ButtonCommand CatalogViewCommand { get; private set; }
        public ButtonCommand ParsingViewCommand { get; private set; }
        public ButtonCommand StorageViewCommand { get; private set; }
        public ButtonCommand PresentsViewCommand { get; private set; }

        public CatalogView CatalogView { get; private set; }
        public ParsingView ParsingView { get; private set; }
        public StorageView StorageView { get; private set; }
        public PresentsView PresentsView { get; private set; }

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

        public MainViewModel(Company company, CatalogViewModel catalogViewModel, StorageViewModel storageViewModel, ParsingViewModel parsingViewModel, PresentsViewModel presentsViewModel)
        {
            Company = company;

            CatalogView = new(catalogViewModel);
            StorageView = new(storageViewModel);
            ParsingView = new(parsingViewModel);
            PresentsView = new(presentsViewModel);

            CurrentView = CatalogView;

            CatalogViewCommand = new(x => ChangeCurrentView(CatalogView));
            StorageViewCommand = new(x => ChangeCurrentView(StorageView));
            ParsingViewCommand = new(x => ChangeCurrentView(ParsingView));
            PresentsViewCommand = new(x => ChangeCurrentView(PresentsView));
        }

        private void ChangeCurrentView(ContentControl contentControl)
        {
            if (CurrentView == contentControl) return;
            CurrentView = contentControl;
        }
    }
}