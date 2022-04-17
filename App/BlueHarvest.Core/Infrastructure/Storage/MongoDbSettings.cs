namespace BlueHarvest.Core.Infrastructure.Storage;

public class MongoDbSettings : IMongoDbSettings
{
   public string? ConnectionString { get; set; }
   public string? DatabaseName { get; set; }
}
