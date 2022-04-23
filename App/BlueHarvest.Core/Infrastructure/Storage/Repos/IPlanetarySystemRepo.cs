using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Infrastructure.Storage.Repos;

public interface IPlanetarySystemRepo : IMongoRepository<PlanetarySystem>
{
   Task<IAsyncCursor<PlanetarySystem>> AllForCluster(ObjectId clusterId);

   Task<(long totalRecords, IEnumerable<PlanetarySystem> data)> FindAllSortByDesignationAscending(
      ObjectId clusterId,
      int page,
      int pageSize = 20);
}
