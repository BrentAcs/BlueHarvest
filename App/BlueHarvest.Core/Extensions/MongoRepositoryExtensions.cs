using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Extensions;

public static class MongoRepositoryExtensions
{
   public static IEnumerable<string?> GetCollectionNames(this IEnumerable<IMongoRepository> repos) =>
      repos.Select(r => r.CollectionName);
   
   public static Task[] InitializeAllIndexesAsync(this IEnumerable<IMongoRepository> repos) =>
      repos.Select(repo => repo.InitializeIndexesAsync()).ToArray().ToArray();
   
   public static Task[] SeedAllDataAsync(this IEnumerable<IMongoRepository> repos) =>
      repos.Select(repo => repo.SeedDataAsync()).ToArray().ToArray();
}
