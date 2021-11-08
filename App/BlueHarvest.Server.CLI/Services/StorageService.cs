using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;
using static System.Console;

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
      ProcessMenu();
   }

   protected override string Title => "Storage Services.";
   protected override void InitMenu()
   {
      ClearActions();
      // AddMenuAction(ConsoleKey.S, "Storage", _storageService.MainMenu);
   }
}

