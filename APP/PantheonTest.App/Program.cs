using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PantheonTest.Application.Contracts.Persistence;
using PantheonTest.Domain.Entities;
using PantheonTest.Identity.Models;
using Serilog;

namespace PantheonTest.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var firstUser = new BankUser()
                    {
                        FirstName = "Test",
                        LastName = "lastname",
                        UserName = "user",
                        Email = "user@test.com",
                        EmailConfirmed = true
                    };
                    var userManager = services.GetRequiredService<UserManager<BankUser>>();
                    var accountRepository = services.GetRequiredService<IAsyncRepository<Account>>();
                    await Identity.Seed.UserCreator.SeedAsync(userManager, firstUser);

                    var user = await userManager.FindByEmailAsync(firstUser.Email);

                    await Persistence.Seed.CreateFirstUserAccount.SeedAsync(accountRepository, user.Id);
                    Log.Information("Application Starting");
                }
                catch (Exception ex)
                {
                    Log.Warning(ex, "An error occurred while starting the application");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
