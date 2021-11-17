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
   private readonly IStarClusterRepo _starClusterRepo;
   private readonly IPlanetarySystemRepo _planetarySystemRepo;

   public StorageService(
      IConfiguration configuration,
      IMediator mediator,
      IMongoContext mongoContext,
      IEnumerable<IMongoRepository> mongoRepos,
      IStarClusterRepo starClusterRepo,
      IPlanetarySystemRepo planetarySystemRepo,
      ILogger<StorageService> logger)
      : base(configuration, logger)
   {
      _mediator = mediator;
      _mongoContext = mongoContext;
      _mongoRepos = mongoRepos;
      _starClusterRepo = starClusterRepo;
      _planetarySystemRepo = planetarySystemRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.D, "Drop Database (Initialize)", DropDatabase);
      AddMenuAction(ConsoleKey.I, "Initialize Database.", InitializeDatabase);
      AddMenuAction(ConsoleKey.L, "List Clusters", ListStarClusters);
      AddMenuAction(ConsoleKey.B, "Build Cluster", BuildStarCluster);
      AddMenuAction(ConsoleKey.T, "Test", TestProc);
   }

   private void DropDatabase()
   {
      ClearScreen("Dropping Database...");

      Write("This is destructive. Are you sure? Type 'DEL' to delete: ");
      var line = ReadLine();
      if (line!.Equals("DEL"))
      {
         WriteLine("Deleting...");
         _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
         WriteLine("Initializing...");
         Task.WaitAll(_mongoRepos.InitializeAllAsync());
      }

      ShowContinue();
   }

   private void InitializeDatabase()
   {
      WriteLine("Initializing...");
      Task.WaitAll(_mongoRepos.InitializeAllAsync());
      ShowContinue();
   }

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

   private void ListStarClusters()
   {
      ClearScreen("Listing Star Clusters...");
      int index = 0;
      foreach (var cluster in _starClusterRepo.All())
      {
         WriteLine($"{++index} - {cluster.Id}: {cluster.Description}");
      }

      ShowContinue();
   }

   private void TestProc()
   {
      ClearScreen("Test...");
      var cluster = _starClusterRepo.FindByNameAsync("test")
         .Result
         .FirstOrDefault();
      DumpStarCluster(cluster.Id);
      ShowContinue();
   }

   private void DumpStarCluster(ObjectId clusterId)
   {
      var cluster = _starClusterRepo.FindById(clusterId.ToString());
      var systems = _planetarySystemRepo.AllForCluster(cluster.Id)
         .Result
         .ToList();

      foreach (var system in systems)
      {
         WriteLine($"{system.Name} - {system.Size!.XDiameter:0.00}");
         foreach (var planet in system.StellarObjects!.OfType<Planet>())
         {
            WriteLine($"   {planet.Name} - {planet.Distance:0.00}");
         }
      }

      ShowContinue();
   }

   private void BuildFullClusterObject()
   {
      var cluster = _starClusterRepo.FindByNameAsync("extralarge")
         .Result
         .FirstOrDefault();
      var systems = _planetarySystemRepo.AllForCluster(cluster.Id)
         .Result
         .ToList();

      var fullCluster = new {Cluster = cluster, Systems = systems}.ToJsonIndented();
      File.WriteAllText(@"c:\t\starcluster.json", fullCluster);
      //WriteLine(fullCluster);
   }
}
