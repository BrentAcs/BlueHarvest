namespace BlueHarvest.Core.Models.Cosmic;

public class Planet : StellarObject
{
   public PlanetType? PlanetType { get; set; }
   public int? Diameter { get; set; }
   public List<Satellite>? Satellites { get; set; }
}
