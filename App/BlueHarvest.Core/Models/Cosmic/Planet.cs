namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// The central object of a satellite system
/// </summary>
public class Planet
{
   public enum PlanetType
   {
      Desert = 1,
      GasGiant,
      IceGiant,
      Ice,
      Lava,
      Oceanic,
      Terrestrial,
   }

   public PlanetType Type { get; set; }
   public double Mass { get; set; }
}
