using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models.Cosmic;

[BsonCollection(CollectionNames.PlanetarySystems)]
public class PlanetarySystem : InterstellarObject, IMongoDocument
{
   public ObjectId Id { get; set; }
   public StarType StarType { get; set; } = StarType.ClassK;
   public double? StarMass { get; set; }
   public Sphere? Size { get; set; } = new(20);
   public List<StellarObject>? StellarObjects { get; set; } = new();
}

// public class Star
// {
//    public StarType StarType { get; set; } = StarType.ClassK;
//    public double? StarMass { get; set; }
// }
