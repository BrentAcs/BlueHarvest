namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// The central object of a satellite system
/// </summary>
public class Planet
{
   public PlanetType Type { get; set; }
   public PlanetaryZone Zone { get; set; }
   public double Distance { get; set; }
   public int Diameter { get; set; }
}
