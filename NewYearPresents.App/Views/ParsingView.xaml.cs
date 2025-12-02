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
        private readonly XlsmParser _parser;
        private readonly AppDbContext _context;

        public ParsingView(XlsmParser parser, AppDbContext context)
        {
            _parser = parser;
            _context = context;
            InitializeComponent();
        }

        private async void Parse_Click(object sender, RoutedEventArgs e)
        {

            ParsedDTO parsedData = _parser.ParseProducts("Прайс-лист 13.10.2025г. (1) (1).xlsm");

            //Сохранение в базу данных
            List<Task> tasks = new List<Task>
            {
                    _context.ProductTypes.AddRangeAsync(parsedData?.ProductTypes),
                    _context.Manufacturers.AddRangeAsync(parsedData.Manufacturers),
                    _context.Products.AddRangeAsync(parsedData.Products),
                    _context.ProductsBoxes.AddRangeAsync(parsedData.ProductsBoxes)
                };

            var whenAllTask = Task.WhenAll(tasks);

            try
            {
                await whenAllTask;
                await _context.SaveChangesAsync();

                MessageBox.Show($"Успешно обработано {parsedData.Products?.Count} записей");
            }
            catch
            {
                // WhenAll бросает только первое исключение
                // Но все исключения доступны через whenAllTask.Exception
                if (whenAllTask.Exception != null)
                {
                    foreach (var ex in whenAllTask.Exception.InnerExceptions)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }



        }
    }
}