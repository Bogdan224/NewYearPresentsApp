using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewYearPresents.App.Infrastructure;
using NewYearPresents.App.ViewModels;
using NewYearPresents.App.Views;
using NewYearPresents.Domain;
using NewYearPresents.Parser;
using System.IO;
using System.Windows;

namespace NewYearPresents.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IServiceProvider _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var dirInfo = new DirectoryInfo(Directory.GetCurrentDirectory());

                IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(dirInfo.Parent!.Parent!.Parent!.FullName)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();

                var serviceCollection = new ServiceCollection();
                ConfigureServices(serviceCollection, configurationBuilder.Build());

                _serviceProvider = serviceCollection.BuildServiceProvider();

                //StringBuilder builder = new StringBuilder();
                //foreach (var service in _serviceProvider.GetServices<object>())
                //{
                //    builder.Append(service.ToString() + "\n\n");
                //}
                //MessageBox.Show(builder.ToString());

                var mainWindow = _serviceProvider.GetRequiredService<MainView>();
                mainWindow.Show();

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                // Обработка ошибок инициализации
                MessageBox.Show($"Ошибка запуска приложения: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
        }

        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Регистрация сервисов
            var config = configuration.GetSection("Project").Get<AppConfig>()!;

            if (config?.Database?.ConnectionString == null)
            {
                throw new InvalidOperationException("Не найдена строка подключения в конфигурации");
            }

            services.AddDataManager(config.Database.ConnectionString!);
            services.AddTransient<XlsmParser>();

            services.AddTransient<MainViewModel>();
            services.AddTransient<MainView>();
        }
    }
}