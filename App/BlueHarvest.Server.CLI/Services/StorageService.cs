using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
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
   private readonly IEntityDesignator _entityDesignator;
   private readonly IStarClusterRepo _starClusterRepo;

   public StorageService(
      IConfiguration configuration,
      ILogger<StorageService> logger,
      IMongoContext mongoContext,
      IEntityDesignator entityDesignator,
      IStarClusterRepo starClusterRepo)
      : base(configuration, logger)
   {
      _mongoContext = mongoContext;
      _entityDesignator = entityDesignator;
      _starClusterRepo = starClusterRepo;
   }

   protected override string Title => "Storage Services.";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.L, "List Collections", ListCollections);
   }
   
   private void ListCollections()
   {
      // CollectionNames

      var type = typeof(CollectionNames);
      var collections = type.GetFields(BindingFlags.Public | BindingFlags.Static)
         .Select(f => f.Name);

      WriteLine("listing collections...");
     
      ShowContinue();
   }
}
