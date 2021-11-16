using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.PlanetarySystems)]
//[KnownType(typeof(Planet))]
public class PlanetarySystem : InterstellarObject, IDocument
{
   public ObjectId Id { get; set; }
   public StarType StarType { get; set; } = StarType.ClassK;
   public Sphere? Size { get; set; } = new(20);
   public List<StellarObject>? StellarObjects { get; set; } = new();
}
