using System.Runtime.Serialization.Formatters.Binary;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Storage.Services;
using BlueHarvest.Core.Utilities;
using MongoDB.Driver;
using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal interface IStorageService : IBaseService
{
}

internal class StorageService : BaseService, IStorageService
{
   private readonly IMongoContext _mongoContext;
   private readonly IEnumerable<IMongoRepository> _monogoRepos;
   private readonly IStarClusterBuilder _starClusterBuilder;
   // private readonly ICollectionsService _collectionsService;
   private readonly IStarClusterRepo _starClusterRepo;

   public StorageService(
      IConfiguration configuration,
      IMongoContext mongoContext,
      IEnumerable<IMongoRepository> monogoRepos,
      IStarClusterBuilder starClusterBuilder,
      // ICollectionsService collectionsService,
      IStarClusterRepo starClusterRepo,
      ILogger<StorageService> logger )
      : base(configuration, logger)
   {
      _mongoContext = mongoContext;
      _monogoRepos = monogoRepos;
      _starClusterBuilder = starClusterBuilder;
      // _collectionsService = collectionsService;
      _starClusterRepo = starClusterRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.D, "Drop Database and re-init", DropDatabase);
      AddMenuAction(ConsoleKey.B, "Create Cluster", BuildStarCluster);
      AddMenuAction(ConsoleKey.L, "List Clusters", ListStarClusters);
      AddMenuAction(ConsoleKey.T, "Test", TestProc);
   }

   private async void DropDatabase()
   {
      await _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
      foreach (var repo in _monogoRepos)
      {
         await repo.InitializeAsync().ConfigureAwait(false);
      }
      
      ShowContinue();
   }

   private void BuildStarCluster()
   {
      var options = StarClusterBuilderOptions.Test;
      _starClusterBuilder.Create(options);
      ShowContinue();
   }
   
   private void ListStarClusters()
   {
      WriteLine("Listing star clusters...");
      foreach (var cluster in _starClusterRepo.All())
      {
         WriteLine($"{cluster.Description}");   
      }
      ShowContinue();
   }

   private async void TestProc()
   {
      

      ShowContinue();
   }
}
