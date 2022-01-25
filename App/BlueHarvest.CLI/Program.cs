// See https://aka.ms/new-console-template for more information

// https://codeopinion.com/why-use-mediatr-3-reasons-why-and-1-reason-not/


using BlueHarvest.CLI.Menus;
using BlueHarvest.Core.Actions;
using BlueHarvest.Core.Extensions;

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
   typeof(BaseHandler).Assembly
};

Host.CreateDefaultBuilder()
   .ConfigureServices(ConfigureServices)
   .ConfigureServices(services =>
   {
      services
         .AddMediatR(assembles)
         .AddBlueHarvestMongo(configuration)
         .AddBlueHarvestCommon(assembles)
         .AddSingleton<MainMenu>()
         ;
   })
   .Build()
   .Services
   .GetService<MainMenu>()
   ?.Execute();

static void ConfigureServices(IServiceCollection services)
{
   // var assembles = new[] {Assembly.GetExecutingAssembly()};
   //
   // services
   //    .AddSingleton<ITest, Test>();
}
