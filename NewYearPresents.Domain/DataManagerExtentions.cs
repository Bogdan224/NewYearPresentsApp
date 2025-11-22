using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NewYearPresents.Domain.Repositories.Abstract;
using NewYearPresents.Domain.Repositories.EntityFramework;

namespace NewYearPresents.Domain
{
    public static class DataManagerExtentions
    {
        public static IServiceCollection AddDataManager(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString)
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<IProductTypesRepository, EFProductTypesRepository>();
            services.AddTransient<IManufacturersRepository, EFManufacturersRepository>();
            services.AddTransient<DataManager>();
            return services;
        }
    }
}