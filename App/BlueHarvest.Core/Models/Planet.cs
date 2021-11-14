namespace BlueHarvest.Core.Models;

public class Planet : StellarObject
{
   public PlanetType? PlanetType { get; set; }
   public int? Diameter { get; set; }
}
