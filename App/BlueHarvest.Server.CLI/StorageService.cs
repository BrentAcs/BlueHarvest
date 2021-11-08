using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Server.CLI;

public interface IStorageService
{
   void Init();
}

public class StorageService : IStorageService
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

   public void Init()
   {

   }
}

