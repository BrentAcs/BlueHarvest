using BlueHarvest.Shared.Infrastructure.Storage;

namespace BlueHarvest.Shared.Models;

[BsonCollection("UserAppStates")]
public class UserAppState : IMongoDocument
{
   public ObjectId Id { get; set; }
   public ObjectId CurrentClusterId { get; set; }
   public ObjectId CurrentPlanetarySystemId { get; set; }
}
