using BlueHarvest.Core.Infastructure.Storage;

namespace BlueHarvest.Core.Models;

public interface IMongoDocument
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   [JsonConverter(typeof(ObjectIdConverter))]
   ObjectId Id { get; set; }
}
