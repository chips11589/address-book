using Application.Common.Interfaces;
using Infrastructure.Common.Constants;
using Infrastructure.Persistence.Dapper;
using Infrastructure.Persistence.EntityFrameworkCore;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DatabaseConstants.CONFIGURATION_DEFAULT_CONNECTION);
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped<IApplicationReadDbConnection, ApplicationReadDbConnection>();
            services.AddScoped<IContactRepository, ContactRepository>();

            return services;
        }
    }
}
