using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NewYearPresents.Domain
{
    public static class DataManagerExtentions
    {
        public static IServiceCollection AddAppDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString)
                    .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));
            return services;
        }
    }
}