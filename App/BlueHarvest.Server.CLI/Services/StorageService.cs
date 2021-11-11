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

   //private readonly IEntityDesignator _entityDesignator;
   private readonly ICollectionsService _collectionsService;
   private readonly IStarClusterRepo _starClusterRepo;

   public StorageService(
      IConfiguration configuration,
      ILogger<StorageService> logger,
      IMongoContext mongoContext,
      IStarClusterBuilder starClusterBuilder,
      //IEntityDesignator entityDesignator,
      ICollectionsService collectionsService,
      IStarClusterRepo starClusterRepo)
      : base(configuration, logger)
   {
      _mongoContext = mongoContext;
      _starClusterBuilder = starClusterBuilder;
      //_entityDesignator = entityDesignator;
      _collectionsService = collectionsService;
      _starClusterRepo = starClusterRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.L, "List Collections", ListCollections);
      AddMenuAction(ConsoleKey.C, "Create Cluster", CreateStarCluster);
   }

   private void ListCollections()
   {
      WriteLine("listing collections...");
      foreach (var name in _collectionsService.CollectionNames)
      {
         WriteLine($"{name}");   
      }
     
      ShowContinue();
   }

   private void CreateStarCluster()
   {
      var options = StarClusterBuilderOptions.Test;
      _starClusterBuilder.Create(options);

   }
}
