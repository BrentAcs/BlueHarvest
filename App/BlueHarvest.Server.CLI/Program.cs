// See https://aka.ms/new-console-template for more information

using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Server.CLI.Services;

namespace BlueHarvest.Server.CLI;

class Program
{
   static async Task Main(string[] args)
   {
      try
      {
         IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(
               $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
               optional: true)
            .AddEnvironmentVariables()
            .Build();
         Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

         await Host.CreateDefaultBuilder(args)
            .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
            .ConfigureLogging(logging =>
            {
            })
            .UseSerilog()
            .ConfigureServices((hostContext, services) =>
            {
               services
                  .AddBlueHarvestCommon(configuration)
                  .AddHostedService<MainService>()
                  .AddSingleton<IStorageService, StorageService>()
                  ;
               
               //  ref: https://stackoverflow.com/questions/57015856/invalidoperationexception-cant-compile-a-newexpression-with-a-constructor-decl
               Utilities.RegisterKnownTypes<StellarObject>();
            })
            .RunConsoleAsync();
         Console.WriteLine("Done");
      }
      catch (Exception ex)
      {
         Console.WriteLine(ex);
      }
   }
}
