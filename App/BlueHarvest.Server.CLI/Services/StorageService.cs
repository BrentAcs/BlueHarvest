using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Server.CLI.Services;

internal interface IStorageService
{
   void MainMenu();
}

internal class StorageService : BaseService, IStorageService
{
   private readonly IMongoContext _mongoContext;
   private readonly IEntityDesignator _entityDesignator;
   private readonly IClusterRepo _clusterRepo;

   public StorageService(
      IMongoContext mongoContext,
      IEntityDesignator entityDesignator,
      IClusterRepo clusterRepo)
   {
      _mongoContext = mongoContext;
      _entityDesignator = entityDesignator;
      _clusterRepo = clusterRepo;
   }

   public void MainMenu()
   {
      InitMenu();
      ProcessMenu();
   }

   protected override string Title => "Storage Services.";

   protected override void InitMenu()
   {
      ClearActions();
      AddMenuAction(ConsoleKey.D1, "List Collections", ListCollections);
   }

   private void ListCollections()
   {
      //var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
      //          .Where(x => typeof(IMongoRepository<Cluster>).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
      //          .Select(x => x.Name).ToList();

      var type = typeof(IMongoRepository<>);

      var types = AppDomain.CurrentDomain.GetAssemblies()
         .SelectMany(x => x.GetTypes())
         .SelectMany(y => y.GetInterfaces())
         .Where(z => z.IsGenericType &&
            type.GetConstructors().Any() &&
            type.IsAssignableFrom(z.GetGenericTypeDefinition()));


   }
}

