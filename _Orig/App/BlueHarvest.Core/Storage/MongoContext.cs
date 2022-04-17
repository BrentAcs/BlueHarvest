using MongoDB.Driver;

namespace BlueHarvest.Core.Storage;

public interface IMongoContext
{
   IMongoDbSettings Settings { get; }
   IMongoClient Client { get; }
   IMongoDatabase Db { get; }
}

public class MongoContext : IMongoContext
{
   public MongoContext(IMongoDbSettings settings)
   {
      Settings = settings;
      Client = new MongoClient(Settings.ConnectionString);
      Db = Client.GetDatabase(Settings.DatabaseName);
   }

   public IMongoDbSettings Settings { get; }
   public IMongoClient Client { get; }
   public IMongoDatabase Db { get; }
}
