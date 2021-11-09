using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal class MainService : BaseService, IHostedService
{
   private readonly IHostApplicationLifetime _appLifetime;
   private readonly IStorageService _storageService;

   public MainService(
      IConfiguration configuration,
      ILogger<MainService> logger,
      IHostApplicationLifetime appLifetime,
      IStorageService storageService)
      :base(configuration, logger)
   {
      _appLifetime = appLifetime;
      _storageService = storageService;
   }

   protected override string Title => "BlueHarvest Server CLI";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.S, "Storage", _storageService.ProcessMenu);
   }

   public Task StartAsync(CancellationToken cancellationToken)
   {
      Logger.LogInformation("MainService Starting...");

      _appLifetime.ApplicationStarted.Register(OnStarted);
      _appLifetime.ApplicationStopped.Register(OnStopped);

      return Task.CompletedTask;
   }

   public Task StopAsync(CancellationToken cancellationToken)
   {
      Logger.LogInformation("MainService Stopping...");
      return Task.CompletedTask;
   }

   private void OnStarted()
   {
      Logger.LogInformation("OnStarted()");

      ProcessMenu();

      WriteLine($"Press any key to quit...");
      ReadKey();
   }

   private void OnStopped() => Logger.LogInformation("OnStopped()");
}
