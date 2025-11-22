using NewYearPresents.Domain;
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
        private readonly XlsmParser _parser;
        private readonly DataManager _dataManager;

        public ParsingView(XlsmParser parser, DataManager dataManager)
        {
            _parser = parser;
            _dataManager = dataManager;
            InitializeComponent();
        }

        private async void Parse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parsedData = _parser.InitialParse("Прайс-лист 13.10.2025г. (1) (1) (1).xlsm");

                // Сохранение в базу данных
                await _dataManager.ProductTypes.SaveProductTypeAsync(parsedData.ProductTypes);
                await _dataManager.Manufacturers.SaveManufacturerAsync(parsedData.Manufacturers);
                await _dataManager.Products.SaveProductAsync(parsedData.Products);

                MessageBox.Show($"Успешно обработано {parsedData.Products?.Count} записей");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}