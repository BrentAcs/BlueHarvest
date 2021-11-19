using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.PlanetDescriptors)]
public class PlanetDescriptor : IMongoDocument
{
   public ObjectId Id { get; set; }
   public PlanetType PlanetType { get; set; }
   public PlanetaryZone[] Zones { get; set; }
   public MinMax<int> Radius { get; set; }
   public MinMax<double> Distance { get; set; }
}
