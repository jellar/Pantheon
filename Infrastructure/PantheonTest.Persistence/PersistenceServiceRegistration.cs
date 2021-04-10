using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Persistence.Repositories;

namespace PantheonTest.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PantheonDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString"),
                    // resilient connections
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    });
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            return services;
        }
    }
}
