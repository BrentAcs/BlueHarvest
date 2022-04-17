using System.Diagnostics;
using System.Reflection;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using BlueHarvest.WinUI.Forms;

namespace BlueHarvest.WinUI;

static class Program
{
   // https://dfederm.com/building-a-console-app-with-.net-generic-host/

   private static IServiceProvider _services;

   [STAThread]
   static void Main()
   {
      ApplicationConfiguration.Initialize();
      Application.ApplicationExit += ApplicationExit;
      Application.ThreadException += ApplicationOnThreadException;

      var host = Host.CreateDefaultBuilder()
         .ConfigureServices((context, svcs) => { ConfigureServices(context.Configuration, svcs); })
         .Build();
      _services = host.Services;

      Application.Run(_services.GetRequiredService<LauncherForm>());
   }

   private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
   {
      var assemblies = new[]
      {
         Assembly.GetExecutingAssembly(),
         Assembly.GetAssembly(typeof(IRootModel)),
      };

      services.AddSingleton(provider => AppOptionsFactory.Create())
         .AddBlueHarvestMongo(configuration)
         .AddBlueHarvestCommon(assemblies)
         .AddSingleton<BuilderMainForm>()
         .AddSingleton<LauncherForm>()
         ;
   }

   private static void ApplicationExit(object? sender, EventArgs e)
   {
      var appOptions = _services.GetRequiredService<IAppOptions>();
      appOptions.Save();
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
