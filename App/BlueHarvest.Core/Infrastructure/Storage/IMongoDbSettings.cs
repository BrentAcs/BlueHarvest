namespace BlueHarvest.Core.Infrastructure.Storage;

public interface IMongoDbSettings
{
   string? ConnectionString { get; set; }
   string? DatabaseName { get; set; }
}
