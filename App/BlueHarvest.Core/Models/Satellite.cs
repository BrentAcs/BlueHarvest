namespace BlueHarvest.Core.Models;

public abstract class Satellite //: OrbitalObject
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
}

public class NaturalSatellite : Satellite
{
}

public class ArtificialSatellite : Satellite
{
}
