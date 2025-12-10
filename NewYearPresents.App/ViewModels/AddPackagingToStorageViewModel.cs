using Microsoft.EntityFrameworkCore;
using NewYearPresents.App.Core;
using NewYearPresents.App.ViewModels.Entities;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Xceed.Wpf.Toolkit.Primitives;

namespace NewYearPresents.App.ViewModels
{
    public class AddPackagingToStorageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        private List<ComboBoxItem> _items;
        private ICollectionView? _filteredItems;

        private string? _searchText;
        private ComboBoxItem? _selectedItem;

        public string? _countText;

        private PackagingViewModel? selectedPackaging;
        private bool propertiesVisibility;

        public ButtonCommand AddButtonCommand { get; private set; }

        public string? CountText
        {
            get => _countText;
            set
            {
                _countText = value;
                OnPropertyChanged(nameof(CountText));
            }
        }

        public PackagingViewModel? SelectedPackaging
        {
            get { return selectedPackaging; }
            set
            {
                selectedPackaging = value;
                OnPropertyChanged("SelectedPackaging");
            }
        }

        public Visibility PropertiesVisibility
        {
            get => propertiesVisibility ? Visibility.Visible : Visibility.Collapsed;
            set
            {
                propertiesVisibility = value == Visibility.Visible ? true : false;
                OnPropertyChanged("PropertiesVisibility");
            }
        }

        public ICollectionView? FilteredItems
        {
            get => _filteredItems;
            set
            {
                if(_filteredItems ==  value) return;
                _filteredItems = value;
                OnPropertyChanged("FilteredItems");
            }
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText == value) return;
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                if (FilteredItems != null)
                {
                    FilteredItems.Refresh();
                }
            }
        }

        public ComboBoxItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                if (_selectedItem == null) 
                    PropertiesVisibility = Visibility.Collapsed;
                else
                {
                    SelectedPackaging = new PackagingViewModel(_context.Packagings.First(x => x.Id == _selectedItem.Id));
                    PropertiesVisibility = Visibility.Visible;
                }
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AddPackagingToStorageViewModel(AppDbContext context)
        {
            // Инициализация данных
            _context = context;
            _items = new List<ComboBoxItem>();

            propertiesVisibility = false;

            AddButtonCommand = new ButtonCommand(async x => await AddPackagingToStorageAsync((Window)x);
        }

        public async Task InitializeAsync()
        {
            _items = await _context.Packagings.Select(x => new ComboBoxItem(x.Id, x.Name)).ToListAsync();

            FilteredItems = CollectionViewSource.GetDefaultView(_items);
            FilteredItems.Filter = ItemFilter;
        }

        private async Task AddPackagingToStorageAsync(Window window)
        {
            try
            {
                if (selectedPackaging == null || CountText == null)
                {
                    throw new ArgumentNullException();
                }
                int count = Convert.ToInt32(CountText);
                
                var packaging = await _context.PackagingsInStorage.FirstOrDefaultAsync(x => x.Packaging == (Packaging)selectedPackaging);
                if (packaging == null)
                {
                    packaging = new PackagingInStorage()
                    {
                        Count = count,
                        Packaging = selectedPackaging
                    };
                }
                else
                    packaging.Count += count;
                await _context.SavePackagingInStorageAsync(packaging);

                window.Close();
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("Заполните все поля!");
            }
            catch
            {
                MessageBox.Show("Заполните все поля правильно!");
            }
        }

        private bool ItemFilter(object item)
        {
            if (string.IsNullOrEmpty(SearchText))
                return true;

            return ((ComboBoxItem)item).ToString().Contains(SearchText,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
