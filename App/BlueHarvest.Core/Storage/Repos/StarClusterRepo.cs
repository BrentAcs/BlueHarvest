using System.Diagnostics;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Storage.Repos;

public interface IStarClusterRepo : IMongoRepository<StarCluster>
{
   Task<IAsyncCursor<StarCluster>> FindByNameAsync(string name);
}

public class StarStarClusterRepo : MongoRepository<StarCluster>, IStarClusterRepo
{
   public StarStarClusterRepo(IMongoContext? mongoContext,
      ILogger<StarStarClusterRepo> logger) : base(mongoContext, logger)
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
            Unique = true
         };
         var builder = Builders<StarCluster>.IndexKeys;
         var index = new CreateIndexModel<StarCluster>(builder.Ascending(i => i.Name), options);
         await Collection.Indexes.CreateOneAsync(index, cancellationToken:cancellationToken).ConfigureAwait(false);
      }
   }

   public Task<IAsyncCursor<StarCluster>> FindByNameAsync(string name) =>
      Task.Run(() =>
      {
         var filter = Builders<StarCluster>
            .Filter.Eq(doc => doc.Name, name);
         var options = new FindOptions<StarCluster, StarCluster>
         {
            Collation = new Collation("en_US", strength: CollationStrength.Primary)
         };

         return Collection.FindAsync(filter, options);
      });
}
