using NewYearPresents.App.Core;
using NewYearPresents.Domain;
using NewYearPresents.Models.DTOs;
using NewYearPresents.Models.Entities;
using NewYearPresents.Models.Extentions;
using NewYearPresents.Parser;
using System.Windows;
using System.Windows.Input;

namespace NewYearPresents.App.ViewModels
{
    
    public class ParsingViewModel
    {
        private readonly ExcelParser _parser;
        private readonly AppDbContext _context;
         
        public ButtonCommand ParseProductsCommand { get; private set; }
        public ButtonCommand ParsePackagingsCommand { get; private set; }

        public ParsingViewModel(ExcelParser parser, AppDbContext context)
        {
            _parser = parser;
            _context = context;

            //TODO: передавать в методы не константную строку, а переменные, которые можно будет менять, выбирая нужный файл из директории
            ParseProductsCommand = new ButtonCommand(async x => await AddToDBParsedProductsBoxFileDTOAsync("Прайс-лист 13.10.2025г. (1) (1).xlsm"));
            ParsePackagingsCommand = new ButtonCommand(async x => await AddToDBParsedPackagingFileDTOAsync("НГ упаковка 2026 (1).xlsx"));
        }

        private async Task AddToDBParsedProductsBoxFileDTOAsync(string filename)
        {            
            var parsedData = await _parser.ParseProductsBoxesFileAsync(filename);

            if (parsedData == null)
            {
                MessageBox.Show("Не удалось загрузить данные");
                return;
            }

            await _context.Database.BeginTransactionAsync();

            await _context.TruncateTableAsync<ProductsBox>("dbo", false);
            await _context.TruncateTableAsync<Product>("dbo", false);
            await _context.TruncateTableAsync<ProductType>("dbo", false);
            await _context.TruncateTableAsync<Manufacturer>("dbo", false);


            //Сохранение в базу данных
            List<Task> tasks = new();
            if (parsedData.ProductTypes != null) tasks.Add(_context.ProductTypes.AddRangeAsync(parsedData.ProductTypes));
            if (parsedData.Manufacturers != null) tasks.Add(_context.Manufacturers.AddRangeAsync(parsedData.Manufacturers));
            if (parsedData.Products != null) tasks.Add(_context.Products.AddRangeAsync(parsedData.Products));
            if (parsedData.ProductsBoxes != null) tasks.Add(_context.ProductsBoxes.AddRangeAsync(parsedData.ProductsBoxes));

            var whenAllTask = Task.WhenAll(tasks);

            try
            {
                await whenAllTask;
                await _context.Database.CommitTransactionAsync();
                await _context.SaveChangesAsync();
                MessageBox.Show($"Успешно обработано {parsedData.Products?.Count} записей");
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
                if (whenAllTask.Exception != null)
                {
                    foreach (var ex in whenAllTask.Exception.InnerExceptions)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }

        private async Task AddToDBParsedPackagingFileDTOAsync(string filename)
        {
            var parsedData = await _parser.ParsePackagingsFileAsync(filename);

            if (parsedData == null)
            {
                MessageBox.Show("Не удалось загрузить данные");
                return;
            }

            await _context.Database.BeginTransactionAsync();

            await _context.TruncateTableAsync<PackagingInStorage>("dbo", false);
            await _context.TruncateTableAsync<Packaging>("dbo", false);

            //Сохранение в базу данных
            List<Task> tasks = new();

            if (parsedData.Packagings != null) tasks.Add(_context.Packagings.AddRangeAsync(parsedData.Packagings));
            if (parsedData.PackagingsInStorage != null) tasks.Add(_context.PackagingsInStorage.AddRangeAsync(parsedData.PackagingsInStorage));

            var whenAllTask = Task.WhenAll(tasks);

            try
            {
                await whenAllTask;
                await _context.Database.CommitTransactionAsync();
                await _context.SaveChangesAsync();
                MessageBox.Show($"Успешно обработано {parsedData.Packagings?.Count} записей");
            }
            catch
            {
                await _context.Database.RollbackTransactionAsync();
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
