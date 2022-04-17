namespace BlueHarvest.Shared.Models.Cosmic;

public class SatelliteSystem : StellarObject
{
   public Planet? Planet { get; set; } = new();
   public List<Satellite> Satellites { get; set; } = new();

   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public IEnumerable<NaturalSatellite> Moons => Satellites.OfType<NaturalSatellite>();

   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public IEnumerable<ArtificialSatellite> Stations => Satellites.OfType<ArtificialSatellite>();
}
