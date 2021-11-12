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
   private readonly IStarClusterBuilder _starClusterBuilder;
   private readonly ICollectionsService _collectionsService;
   private readonly IStarClusterRepo _starClusterRepo;

   public StorageService(
      IConfiguration configuration,
      ILogger<StorageService> logger,
      IMongoContext mongoContext,
      IStarClusterBuilder starClusterBuilder,
      ICollectionsService collectionsService,
      IStarClusterRepo starClusterRepo)
      : base(configuration, logger)
   {
      _mongoContext = mongoContext;
      _starClusterBuilder = starClusterBuilder;
      _collectionsService = collectionsService;
      _starClusterRepo = starClusterRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.C, "Create Cluster", CreateStarCluster);
      AddMenuAction(ConsoleKey.L, "List Clusters", ListStarClusters);
      AddMenuAction(ConsoleKey.T, "Test", TestProc);
   }

   private void CreateStarCluster()
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
      //_starClusterRepo.
      var col = _mongoContext.Db.GetCollection<StarCluster>(CollectionNames.StarClusters);
      var indexes = await col.Indexes.ListAsync().ConfigureAwait(false);
      var exists = await indexes.AnyAsync().ConfigureAwait(false);
      if (!exists)
      {
         var options = new CreateIndexOptions();
         options.Collation = new Collation("en_US",
            false,
            new Optional<CollationCaseFirst?>(CollationCaseFirst.Off),
            new Optional<CollationStrength?>(CollationStrength.Primary));

         //var index = new CreateIndexModel<StarCluster>("{ AgencyEnterprise : 1, CodeName : 1 }", options);
         var index = new CreateIndexModel<StarCluster>($"{{ {nameof(StarCluster.Description)} : 1 }}", options);
         col.Indexes.CreateOne(index);      }

   }
}
