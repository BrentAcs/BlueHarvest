using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Shared.Models.Cosmic;

public class PlanetarySystem : InterstellarObject, IMongoDocument
{
   public ObjectId Id { get; set; }
   public Star? Star { get; set; } = new();
   public Sphere? Size { get; set; } = new(0);
   public List<StellarObject>? StellarObjects { get; set; } = new();

   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public IEnumerable<SatelliteSystem>? SatelliteSystems => StellarObjects?.OfType<SatelliteSystem>();

   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public IEnumerable<AsteroidField>? AsteroidFields => StellarObjects?.OfType<AsteroidField>();
}
