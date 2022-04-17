namespace BlueHarvest.Core.Infrastructure.Storage;

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
