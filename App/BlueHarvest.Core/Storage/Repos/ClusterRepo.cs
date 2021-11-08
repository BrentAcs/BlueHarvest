using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Storage.Repos;

public interface IClusterRepo : IMongoRepository<Cluster>
{
}

public class ClusterRepo : MongoRepository<Cluster>, IClusterRepo
{
   public ClusterRepo(IMongoContext mongoContext) : base(mongoContext)
   {
   }
}
