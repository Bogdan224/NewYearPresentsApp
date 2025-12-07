using Microsoft.EntityFrameworkCore;
using NewYearPresents.App.Core;
using NewYearPresents.App.ViewModels.Entities;
using NewYearPresents.Domain;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NewYearPresents.App.ViewModels
{
    public class AddProductsBoxToStorageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        private List<ComboBoxItem> _items;
        private ICollectionView? _filteredItems;

        private string? _searchText;
        private ComboBoxItem? _selectedItem;

        private Window _window;
        private ComboBox _comboBox;
        private TextBox _countTextBox;

        private ProductsBoxViewModel? selectedProductsBox;
        private bool propertiesVisibility;

        public ButtonCommand AddButtonCommand { get; private set; }

        public ProductsBoxViewModel? SelectedProductsBox 
        { 
            get { return selectedProductsBox; } 
            set
            {
                selectedProductsBox = value;
                OnPropertyChanged("SelectedProductsBox");
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
                _filteredItems = value;
                OnPropertyChanged("FilteredItems");
            }
        }

        public string? SearchText
        {
            get => _searchText;
            set
            {
                if (value == null) return;
                _searchText = value;
                OnPropertyChanged();
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
                if (value == null) return;
                _selectedItem = value;
                SelectedProductsBox = new ProductsBoxViewModel(_context.ProductsBoxes.First(x => x.Id == _selectedItem.Id));
                PropertiesVisibility = Visibility.Visible;
                OnPropertyChanged();
            }
        }

        public AddProductsBoxToStorageViewModel(AppDbContext context)
        {
            // Инициализация данных
            _context = context;
            _items = new List<ComboBoxItem>();

            propertiesVisibility = false;

            AddButtonCommand = new ButtonCommand(async x => await AddProductsBoxesToStorageAsync());
        }

        public async Task InitializeAsync(ComboBox comboBox, TextBox countTextBox, Window window)
        {
            _items = await _context.ProductsBoxes.Select(x => new ComboBoxItem(x.Id, x.Product.Name)).ToListAsync();

            _window = window;
            _comboBox = comboBox;
            _countTextBox = countTextBox;

            FilteredItems = CollectionViewSource.GetDefaultView(_items);
            FilteredItems.Filter = ItemFilter;
        }

        private async Task AddProductsBoxesToStorageAsync()
        {
            try
            {
                if (selectedProductsBox == null || _countTextBox?.Text == null)
                {
                    throw new ArgumentNullException();
                }
                int count = Convert.ToInt32(_countTextBox.Text);

                var product = new ProductsBoxInStorage()
                {
                    Count = count,
                    ProductsBox = selectedProductsBox
                };

                await _context.SaveProductsBoxInStorageAsync(product);
                _window.Close();
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

    public class ComboBoxItem(int id, string? name)
    {
        public int Id { get; set; } = id;
        public string? Name { get; set; } = name;

        public override string ToString()
        {
            return Name ?? "";
        }
    }

}
