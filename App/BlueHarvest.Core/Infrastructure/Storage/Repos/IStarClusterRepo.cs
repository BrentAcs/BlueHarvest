using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Infrastructure.Storage.Repos;

public interface IStarClusterRepo : IMongoRepository<StarCluster>
{
   Task<IAsyncCursor<StarCluster>> FindByNameAsync(string name, CancellationToken cancellationToken = default);
   Task<bool> ClusterExistsByNameAsync(string name, CancellationToken cancellationToken = default);
}
