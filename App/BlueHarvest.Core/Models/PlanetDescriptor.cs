using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.PlanetDescriptors)]
public class PlanetDescriptor : IMongoDocument
{
   public ObjectId Id { get; set; }
   public PlanetType PlanetType { get; set; }
   public PlanetaryZone[] Zones { get; set; } = Array.Empty<PlanetaryZone>();
   public MinMax<int> Radius { get; set; } = new();
   public MinMax<double> Distance { get; set; } = new();
}
