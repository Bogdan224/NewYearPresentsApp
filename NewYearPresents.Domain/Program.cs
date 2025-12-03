using NewYearPresents.Models.Extentions;

namespace NewYearPresents.Domain;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

        //Подключаем в конфигурацию файл appsettings.json
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(dirInfo.FullName)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();

        //Оборачиваем секцию Project в объектную форму для удобства
        IConfiguration configuration = configurationBuilder.Build();
        string connectionString = configuration.GetSection("Database").GetSection("ConnectionString").Value!;

        //Добавляем зависимости
        builder.Services.AddAppDbContext(connectionString);

        var app = builder.Build();

        app.Run();
    }
}