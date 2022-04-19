// See https://aka.ms/new-console-template for more information

using BlueHarvest.Core.Extensions;
using BlueHarvest.PoC.CLI.Actions;
using BlueHarvest.PoC.CLI.Menus;

if (OperatingSystem.IsWindows())
{
   Console.WindowWidth = 120;
   Console.WindowHeight = 40;
}

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
         .AddSingleton<FactoryTestAction>()
         .AddSingleton<DbUtilityAction>()
         .AddSingleton<CreateStarClusterAction>()
         .AddSingleton<ListStarClustersAction>()
         .AddSingleton<ListPlanetarySystemsAction>()
         .AddSingleton<MainMenu>()
         ;
   })
   .Build()
   .Services
   .GetService<MainMenu>()
   ?.Execute();
