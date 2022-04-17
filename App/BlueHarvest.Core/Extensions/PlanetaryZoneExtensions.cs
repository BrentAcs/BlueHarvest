using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Extensions;

public static class PlanetaryZoneExtensions
{
   public static PlanetaryZone IdentifyPlanetaryZone(this double distance) =>
      distance switch
      {
         <= 0.2499 => PlanetaryZone.Inner,
         <= 1.499 => PlanetaryZone.InnerHabitable,
         <= 3.499 => PlanetaryZone.Habitable,
         <= 4.999 => PlanetaryZone.OuterHabitable,
         _ => PlanetaryZone.Outer
      };

}
