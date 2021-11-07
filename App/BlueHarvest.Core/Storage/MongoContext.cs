using MongoDB.Driver;

namespace BlueHarvest.Core.Storage;

public interface IMongoContext
{
   IMongoClient Client { get; }
   IMongoDatabase Db { get; }
}

public class MongoContext : IMongoContext
{
   public MongoContext(IMongoDbSettings settings)
   {
      Client = new MongoClient(settings.ConnectionString);
      Db = Client.GetDatabase(settings.DatabaseName);
   }

   public IMongoClient Client { get; }
   public IMongoDatabase Db { get; }
}
