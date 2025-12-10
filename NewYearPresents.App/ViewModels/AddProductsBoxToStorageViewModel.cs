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

        private string? _countText;

        private ProductsBoxViewModel? selectedProductsBox;
        private bool propertiesVisibility;

        public ButtonCommand AddButtonCommand { get; private set; }

        public string? CountText
        {
            get => _countText;
            set
            {
                _countText = value;
                OnPropertyChanged(nameof(_countText));
            }
        }

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
                if (value == null) return;
                _selectedItem = value;
                SelectedProductsBox = new ProductsBoxViewModel(_context.ProductsBoxes.First(x => x.Id == _selectedItem.Id));
                PropertiesVisibility = Visibility.Visible;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public AddProductsBoxToStorageViewModel(AppDbContext context)
        {
            // Инициализация данных
            _context = context;
            _items = new List<ComboBoxItem>();

            propertiesVisibility = false;

            AddButtonCommand = new ButtonCommand(async x => await AddProductsBoxesToStorageAsync((Window)x));
        }

        public async Task InitializeAsync()
        {
            _items = await _context.ProductsBoxes.Select(x => new ComboBoxItem(x.Id, x.Product.Name)).ToListAsync();

            FilteredItems = CollectionViewSource.GetDefaultView(_items);
            FilteredItems.Filter = ItemFilter;
        }

        private async Task AddProductsBoxesToStorageAsync(Window window)
        {
            try
            {
                if (selectedProductsBox == null || CountText == null)
                {
                    throw new ArgumentNullException();
                }
                int count = Convert.ToInt32(CountText);

                var product = await _context.ProductsBoxesInStorage.FirstOrDefaultAsync(x => x.ProductsBox == (ProductsBox)selectedProductsBox);
                if (product == null)
                {
                    product = new ProductsBoxInStorage()
                    {
                        Count = count,
                        ProductsBox = selectedProductsBox
                    };
                }
                else
                    product.Count += count;

                await _context.SaveProductsBoxInStorageAsync(product);
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
