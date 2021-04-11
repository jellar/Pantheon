using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Application.Models;
using PantheonTest.Infrastructure.CurrencyService;
using PantheonTest.Infrastructure.FileExport;

namespace PantheonTest.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencyConvertApiSettings>(configuration.GetSection("CurrencyConvertApiSettings"));
            services.AddHttpClient("RatesApiClient", config =>
            {
                config.DefaultRequestHeaders.Add("accept", "application/json");
            });
            services.AddTransient<ICurrencyConvertService, CurrencyConvertService>();
            services.AddTransient<ICsvExporter, CsvExporter>();
            return services;
        }
    }
}
