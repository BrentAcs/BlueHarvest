using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.PlanetDescriptors)]
public class PlanetDescriptor : IDocument
{
   public ObjectId Id { get; set; }
   public PlanetType PlanetType { get; set; }
   public PlanetaryZone[] Zones { get; set; }
   public MinMax<int> Radius { get; set; }
   public MinMax<double> Distance { get; set; }
}
