using NewYearPresents.App.Core;
using NewYearPresents.Domain;
using NewYearPresents.Models.DTOs;
using NewYearPresents.Parser;
using System.Windows;
using System.Windows.Input;

namespace NewYearPresents.App.ViewModels
{
    public class ParsingViewModel
    {
        private readonly XlsmParser _parser;
        private readonly AppDbContext _context;

        public string? Filename { get; set; }
 
        public ButtonCommand ParseCommand { get; private set; }

        public ParsingViewModel(XlsmParser parser, AppDbContext context)
        {
            _parser = parser;
            _context = context;

            ParseCommand = new ButtonCommand(async x =>
            {
                await ParseXlsmFileAsync();
            });
        }

        private async Task ParseXlsmFileAsync()
        {
            if (Filename == null)
            {
                MessageBox.Show("Файл не задан");
                return;
            }
            var parsedData = await _parser.ParseProductsBoxesFileAsync(Filename);

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
