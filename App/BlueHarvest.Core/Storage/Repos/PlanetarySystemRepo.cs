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
   public PlanetarySystemRepo(IMongoContext mongoContext) : base(mongoContext)
   {
   }
   public override async Task InitializeIndexesAsync()
   {
      await base.InitializeIndexesAsync().ConfigureAwait(false);

      var indexes = await Collection.Indexes.ListAsync().ConfigureAwait(false);
      var exists = await indexes.AnyAsync().ConfigureAwait(false);
      if (!exists)
      {
         var options = new CreateIndexOptions
         {
            Collation = new Collation("en_US",
               false,
               new Optional<CollationCaseFirst?>(CollationCaseFirst.Off),
               new Optional<CollationStrength?>(CollationStrength.Primary))
         };

         var index =
            new CreateIndexModel<PlanetarySystem>($"{{ {nameof(PlanetarySystem.Name)} : 1 }}", options);
         await Collection.Indexes.CreateOneAsync(index).ConfigureAwait(false);
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
