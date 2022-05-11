using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlueHarvest.WinUI;

static class Program
{
   /// <summary>
   ///  The main entry point for the application.
   /// </summary>
   [STAThread]
   static void Main()
   {
      // To customize application configuration such as set high DPI settings or default font,
      // see https://aka.ms/applicationconfiguration.
      //ApplicationConfiguration.Initialize();
      //Application.Run(new Form1());

      Application.SetHighDpiMode(HighDpiMode.SystemAware);
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      var host = CreateHostBuilder().Build();
      ServiceProvider = host.Services;

      Application.Run(ServiceProvider.GetRequiredService<MainForm>());
   }
   public static IServiceProvider ServiceProvider { get; private set; }
   static IHostBuilder CreateHostBuilder()
   {
      var assemblies = new[] {
         Assembly.GetExecutingAssembly(),
      };

      return Host.CreateDefaultBuilder()
          .ConfigureServices((context, services) =>
          {
             services.AddMediatR(assemblies.ToArray());
             //services.AddHttpClient<IStarClusterService, StarClusterService>(client => client.BaseAddress = new Uri("https://localhost:7013/"));
             services.AddTransient<IStarClusterApi, StarClusterApi>();
             services.AddTransient<IAppSettings, AppSettings>();
             services.AddTransient<MainForm>();
          });
   }
}

