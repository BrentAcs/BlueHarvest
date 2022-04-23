using BlueHarvest.Core.Infrastructure.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection("UserAppStates")]
public class UserAppState : IMongoDocument
{
   public ObjectId Id { get; set; }
   public ObjectId CurrentClusterId { get; set; }
   public ObjectId CurrentPlanetarySystemId { get; set; }
}
