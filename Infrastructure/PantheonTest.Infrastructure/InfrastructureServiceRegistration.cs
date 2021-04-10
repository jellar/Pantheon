using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PantheonTest.Application.Contracts.Infrastructure;
using PantheonTest.Infrastructure.FileExport;

namespace PantheonTest.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICsvExporter, CsvExporter>();
            return services;
        }
    }
}
