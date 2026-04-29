using ESFE.DataAccess.Interfaces;
using ESFE.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ESFE.DataAccess
{
    public static class DataAccessExtensions
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QuotationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DbConnection") ??
                throw new InvalidOperationException("connection string 'DbConnection not found '"))
            );

            services.AddTransient(typeof(IEfRepository<>), typeof(EfRepository<>));

            return services;
        }
    }
}
