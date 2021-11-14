using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

// public interface IPlanetarySystem
// {
// }

[BsonCollection(CollectionNames.PlanetarySystems)]
[KnownType(typeof(Planet))]
public class PlanetarySystem : InterstellarObject //, IPlanetarySystem
{
   public StarType StarType { get; set; } = StarType.ClassK;
   public Sphere Size { get; set; } = new(20);
   public List<StellarObject> Objects { get; set; } = new();
}
