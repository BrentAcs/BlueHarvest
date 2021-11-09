using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;
using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal class MainService : BaseService
{
   private readonly IStorageService _storageService;

   public MainService(
      IConfiguration configuration,
      ILogger<MainService> logger,
      IHostApplicationLifetime appLifetime,
      IStorageService storageService)
      :base(configuration, logger, appLifetime)
   {
      _storageService = storageService;
   }

   protected override string Title => "BlueHarvest Server CLI";

   protected override void InitMenu()
   {
      ClearActions();
      AddMenuAction(ConsoleKey.S, "Storage", _storageService.MainMenu);
   }

   public override Task StartAsync(CancellationToken cancellationToken)
   {
      _ = base.StartAsync(cancellationToken);

      Logger.LogInformation("MainService Starting...");

      AppLifetime.ApplicationStarted.Register(OnStarted);
      AppLifetime.ApplicationStopped.Register(OnStopped);

      return Task.CompletedTask;
   }

   public override Task StopAsync(CancellationToken cancellationToken)
   {
      Logger.LogInformation("MainService Stopping...");

      return base.StopAsync(cancellationToken);
   }

   private void OnStarted()
   {
      Logger.LogInformation("OnStarted()");

      ProcessMenu();

      WriteLine($"Press any key to quit...");
      ReadKey();
      AppLifetime.StopApplication();
   }

   private void OnStopped() => Logger.LogInformation("OnStopped()");

}
