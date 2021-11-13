using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Extensions;

public static class MongoRepositoryExtensions
{
   public static IEnumerable<string?> GetCollectionNames(this IEnumerable<IMongoRepository> repos) =>
      repos.Select(r => r.CollectionName);
   
   public static Task[] InitializeAllAsync(this IEnumerable<IMongoRepository> repos) =>
      repos.Select(repo => repo.InitializeAsync()).ToArray().ToArray();
}
