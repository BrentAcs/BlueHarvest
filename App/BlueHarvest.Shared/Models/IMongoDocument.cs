using BlueHarvest.Shared.Infrastructure.Storage;

namespace BlueHarvest.Shared.Models;

public interface IMongoDocument
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   [JsonConverter(typeof(ObjectIdConverter))]
   ObjectId Id { get; set; }
}
