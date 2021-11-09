using System.Runtime.Serialization;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.PlanetarySystems)]
[KnownType(typeof(Planet))]
public class PlanetarySystem : InterstellarObject
{
   public StarType StarType { get; set; } = StarType.ClassK;
   public Sphere Size { get; set; } = new(20);
   public List<StellarObject> Objects { get; set; } = new();
}
