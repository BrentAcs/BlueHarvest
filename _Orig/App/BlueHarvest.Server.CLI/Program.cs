﻿// See https://aka.ms/new-console-template for more information

using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Server.CLI.Services;
using MediatR;
using static System.Console;

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
               var assemblies = new[]
               {
                  Assembly.GetAssembly(typeof(IRootModel)),
                  Assembly.GetAssembly(typeof(MainService)),
               };

               services
                  .AddBlueHarvestMongo(configuration)
                  .AddBlueHarvestCommon(assemblies)
                  //.AddMediatR(typeof(MainService))
                  .AddHostedService<MainService>()
                  .AddSingleton<IStorageService, StorageService>()
                  .AddSingleton<ITypeService, TypeService>()
                  ;
            })
            .RunConsoleAsync();
      }
      catch (Exception ex)
      {
         WriteLine(ex);
         WriteLine("Press any key to exit.");
         ReadKey(true);
      }
   }
}