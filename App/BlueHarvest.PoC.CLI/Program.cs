// See https://aka.ms/new-console-template for more information

using BlueHarvest.Core.Extensions;
using BlueHarvest.PoC.CLI.Actions;
using Serilog;

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

var assembles = new[]
{
   Assembly.GetExecutingAssembly(),
   //typeof(BaseHandler).Assembly
};

Host.CreateDefaultBuilder()
   .ConfigureServices(services =>
   {
      services
         // .AddMediatR(assembles)
         .AddBlueHarvestMongo(configuration)
         .AddBlueHarvestCommon(assembles)
         .AddSingleton<TestFactoryActions>()
         .AddSingleton<MainMenu>()
         ;
   })
   .Build()
   .Services
   .GetService<MainMenu>()
   ?.Execute();
