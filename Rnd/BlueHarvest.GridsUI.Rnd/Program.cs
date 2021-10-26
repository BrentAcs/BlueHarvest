using System.Diagnostics;
using BlueHarvest.Core.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlueHarvest.GridsUI.Rnd;

static class Program
{
   // https://dfederm.com/building-a-console-app-with-.net-generic-host/
   
   [STAThread]
   static void Main()
   {
      ApplicationConfiguration.Initialize();
      Application.ApplicationExit += ApplicationExit;
      Application.ThreadException += ApplicationOnThreadException;

      var host = Host.CreateDefaultBuilder()
         .ConfigureServices((context, svcs) => { ConfigureServices(context.Configuration, svcs); })
         .Build();
      var services = host.Services;
      
      Application.Run(services.GetRequiredService<MainForm>());
   }

   // ReSharper disable once UnusedParameter.Local
   private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
   {
      var appOptions = new AppOptions();
      var optionsPath= configuration[ "OptionsPath" ];
      if (File.Exists(optionsPath))
      {
         appOptions = optionsPath.FromJsonFile<AppOptions>();
      }
      
      services
         //.AddDangerBallCommon(configuration)
         // .AddSingleton(provider => UserSettingsFactory.Create())
         // .AddScoped<IHexMapRenderer, HexMapRenderer>()
         // .AddScoped<IHexMapEditorPublisher, HexMapEditorPublisher>()
         // .AddSingleton<ITerrainService, TerrainService>()
         .AddSingleton<AppOptions>(appOptions)
         .AddSingleton<MainForm>()
         // .AddSingleton<ClusterExplorerForm>()
         // .AddSingleton<HexMapEditorForm>()
         ;
   }

   private static void ApplicationExit(object? sender, EventArgs e)
   {
      
   }

   private static void ApplicationOnThreadException(object sender, ThreadExceptionEventArgs args)
   {
      Debug.WriteLine($"Exception: {args.Exception.Message}");
      Debug.WriteLine(args.Exception.StackTrace);

      MessageBox.Show(
         $@"Unhandled Exception!{Environment.NewLine}{Environment.NewLine}{
            args.Exception.Message}{Environment.NewLine}{args.Exception.StackTrace}",
         @"Error!",
         MessageBoxButtons.OK,
         MessageBoxIcon.Error);
   }
}
