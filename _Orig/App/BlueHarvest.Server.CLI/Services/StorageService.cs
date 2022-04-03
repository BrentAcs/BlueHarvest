using BlueHarvest.Core.Actions.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using MediatR;
using MongoDB.Bson;
using MongoDB.Driver;
using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal interface IStorageService : IBaseService
{
}

internal class StorageService : BaseService, IStorageService
{
   private readonly IMediator _mediator;
   private readonly IMongoContext _mongoContext;
   private readonly IEnumerable<IMongoRepository> _mongoRepos;
   // private readonly IStarClusterRepo _starClusterRepo;
   // private readonly IPlanetarySystemRepo _planetarySystemRepo;

   public StorageService(
      IConfiguration configuration,
      IMediator mediator,
      IMongoContext mongoContext,
      IEnumerable<IMongoRepository> mongoRepos,
      // IStarClusterRepo starClusterRepo,
      // IPlanetarySystemRepo planetarySystemRepo,
      ILogger<StorageService> logger)
      : base(configuration, logger)
   {
      _mediator = mediator;
      _mongoContext = mongoContext;
      _mongoRepos = mongoRepos;
      // _starClusterRepo = starClusterRepo;
      // _planetarySystemRepo = planetarySystemRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.D, "Drop DB", DropDb);
      AddMenuAction(ConsoleKey.I, "Initialize DB.", InitializeDb);
      AddMenuAction(ConsoleKey.R, "Reset DB (drop and initialize).", ResetDb);
      AddMenuAction(ConsoleKey.L, "List Clusters", ListStarClusters);
      AddMenuAction(ConsoleKey.B, "Build Cluster", BuildStarCluster);
      //AddMenuAction(ConsoleKey.T, "Test", TestProc);
   }

   private void DropDb() =>
      DropDb(false);

   private void DropDb(bool initDb = false)
   {
      ClearScreen("Dropping Database...");

      Write("This is destructive. Are you sure? Type 'DEL' to delete: ");
      var line = ReadLine();
      if (line!.Equals("DEL"))
      {
         WriteLine("Deleting...");
         _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);

         if (initDb)
         {
            InitializeDb();
            return;
         }
      }

      ShowContinue();
   }

   private void InitializeDb()
   {
      WriteLine("Initializing...");
      Task.WaitAll(_mongoRepos.InitializeAllIndexesAsync());
      WriteLine("Seeding...");
      Task.WaitAll(_mongoRepos.SeedAllDataAsync());
      ShowContinue();
   }

   private void ResetDb() =>
      DropDb(true);

   private async void BuildStarCluster()
   {
      ClearScreen("Building Star Cluster...");

      try
      {
         WriteLine("1. Extra Large");
         WriteLine("2. Large");
         WriteLine("3. Medium");
         WriteLine("4. Small");
         WriteLine("5. Test (default)");
         WriteLine("Q. Cancel");

         var key = ShowPrompt("Select base options");
         if (key == ConsoleKey.Q)
            return;

         StarClusterBuilderOptions options = key switch
         {
            ConsoleKey.D1 => StarClusterBuilderOptions.ExtraLarge,
            ConsoleKey.D2 => StarClusterBuilderOptions.Large,
            ConsoleKey.D3 => StarClusterBuilderOptions.Medium,
            ConsoleKey.D4 => StarClusterBuilderOptions.Small,
            _ => StarClusterBuilderOptions.Test
         };

         WriteLine("Building...");
         _ = await _mediator.Send((StarClusterBuilder.Request)options);
      }
      catch (Exception ex)
      {
         WriteLine(ex);
      }
      finally
      {
         ShowContinue();
      }
   }

   private async void ListStarClusters()
   {
      ClearScreen("Listing Star Clusters...");
      int index = 0;
      var clusters = await _mediator.Send(new GetAllStarClusters.Request()).ConfigureAwait(false);
      foreach (var cluster in clusters)
      {
         WriteLine($"{++index} - {cluster.Name}: {cluster.Description}");
      }

      ShowContinue();
   }

   private void TestProc()
   {
      ClearScreen("Test...");

      try
      {
      }
      catch (MongoWriteException ex)
      {
         WriteLine($"Write Error: {ex.Message}");
      }
      catch (Exception ex)
      {
         WriteLine($"Error: {ex.Message}");
      }

      ShowContinue();
   }
}
