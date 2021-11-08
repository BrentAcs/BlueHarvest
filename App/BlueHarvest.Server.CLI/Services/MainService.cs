using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;
using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal class MainService : BaseService
{
   private readonly IConfiguration _configuration;
   private readonly ILogger<MainService> _logger;
   private readonly IHostApplicationLifetime _appLifetime;
   private readonly IStorageService _storageService;

   public MainService(
      IConfiguration configuration,
      ILogger<MainService> logger,
      IHostApplicationLifetime appLifetime,
      IStorageService storageService)
   {
      _configuration = configuration;
      _logger = logger;
      _appLifetime = appLifetime;
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

      _logger.LogInformation("MainService Starting...");

      _appLifetime.ApplicationStarted.Register(OnStarted);
      _appLifetime.ApplicationStopped.Register(OnStopped);

      return Task.CompletedTask;
   }

   private void OnStarted()
   {
      _logger.LogInformation("OnStarted()");

      ProcessMenu();

      WriteLine($"Press any key to quit...");
      ReadKey();
      _appLifetime.StopApplication();
   }

   private void OnStopped() => _logger.LogInformation("OnStopped()");

   public override Task StopAsync(CancellationToken cancellationToken)
   {
      _logger.LogInformation("MainService Stopping...");

      return base.StopAsync(cancellationToken);
   }
}
