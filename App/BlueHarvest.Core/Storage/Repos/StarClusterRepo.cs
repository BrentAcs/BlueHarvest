using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Storage.Repos;

public interface IStarClusterRepo : IMongoRepository<StarCluster>
{
}

public class StarStarClusterRepo : MongoRepository<StarCluster>, IStarClusterRepo
{
   public StarStarClusterRepo(IMongoContext mongoContext) : base(mongoContext)
   {
   }
}
