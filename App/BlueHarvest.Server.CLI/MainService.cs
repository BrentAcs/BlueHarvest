﻿using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Server.CLI;

internal class MainService : IHostedService
{
   private readonly IConfiguration _configuration;
   private readonly ILogger<MainService> _logger;
   private readonly IHostApplicationLifetime _appLifetime;
   private readonly IMongoContext _mongoContext;
   private readonly IEntityDesignator _entityDesignator;

   public MainService(
      IConfiguration configuration,
      ILogger<MainService> logger,
      IHostApplicationLifetime appLifetime,
      IStorageService storageService)
   {
      _configuration = configuration;
      _logger = logger;
      _appLifetime = appLifetime;
   }

   public Task StartAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("MainService Starting...");

      _appLifetime.ApplicationStarted.Register(OnStarted);
      _appLifetime.ApplicationStopped.Register(OnStopped);

      return Task.CompletedTask;
   }

   private void OnStarted()
   {
      _logger.LogInformation("OnStarted()");


      //_mongoContext.Db.DropCollection("Test");




      Console.WriteLine($"Press any key to quit...");
      Console.ReadKey();
      _appLifetime.StopApplication();
   }

   private void OnStopped() => _logger.LogInformation("OnStopped()");

   public Task StopAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("MainService Stopping...");

      return Task.CompletedTask;
   }
}