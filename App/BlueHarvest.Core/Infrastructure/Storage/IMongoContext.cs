namespace BlueHarvest.Core.Infrastructure.Storage;

public interface IMongoContext
{
   IMongoDbSettings Settings { get; }
   IMongoClient Client { get; }
   IMongoDatabase Db { get; }
}
