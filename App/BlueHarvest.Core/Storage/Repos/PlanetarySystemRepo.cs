using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Storage.Repos;

public interface IPlanetarySystemRepo : IMongoRepository<PlanetarySystem>
{
   Task<IAsyncCursor<PlanetarySystem>> AllForCluster(ObjectId clusterId);

   Task<(long totalRecords, IEnumerable<PlanetarySystem> data)> FindAllSortByDesignationAscending(
      ObjectId clusterId,
      int page,
      int pageSize = 20);
}

public class PlanetarySystemRepo : MongoRepository<PlanetarySystem>, IPlanetarySystemRepo
{
   public PlanetarySystemRepo(IMongoContext? mongoContext,
      ILogger<PlanetarySystemRepo> logger) : base(mongoContext, logger)
   {
   }

   public override async Task InitializeIndexesAsync(CancellationToken cancellationToken = default)
   {
      await base.InitializeIndexesAsync(cancellationToken).ConfigureAwait(false);

      var indexes = await Collection.Indexes.ListAsync(cancellationToken).ConfigureAwait(false);
      var exists = await indexes.AnyAsync(cancellationToken).ConfigureAwait(false);
      if (!exists)
      {
         var options = new CreateIndexOptions
         {
            Collation = new Collation("en_US",
               false,
               new Optional<CollationCaseFirst?>(CollationCaseFirst.Off),
               new Optional<CollationStrength?>(CollationStrength.Primary)),
            Unique = true,
         };
         var builder = Builders<PlanetarySystem>.IndexKeys;
         var keys = builder
            .Ascending(i => i.ClusterId)
            .Ascending(i => i.Name);
         var index = new CreateIndexModel<PlanetarySystem>(keys, options);

         await Collection.Indexes.CreateOneAsync(index, cancellationToken:cancellationToken).ConfigureAwait(false);
      }
   }

   public async Task<IAsyncCursor<PlanetarySystem>> AllForCluster(ObjectId clusterId)
   {
      var filter = Builders<PlanetarySystem>.Filter.Eq(s => s.ClusterId, clusterId);

      return await Collection.FindAsync(filter);
   }

   public async Task<(long totalRecords, IEnumerable<PlanetarySystem> data)> FindAllSortByDesignationAscending(
      ObjectId clusterId,
      int page,
      int pageSize = 20)
   {
      var filter = Builders<PlanetarySystem>.Filter.Eq(s => s.ClusterId, clusterId);
      var sort = Builders<PlanetarySystem>.Sort.Ascending(s => s.Name);

      return await AggregateByPage(filter, sort, page, pageSize);
   }
}
