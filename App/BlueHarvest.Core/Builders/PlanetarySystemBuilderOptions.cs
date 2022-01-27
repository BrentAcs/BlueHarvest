using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Builders;

public class PlanetarySystemBuilderOptions
{
   // 149,597,870,700 meters in an AU 

   // Min/Max radius of solar system in AU
   public MinMax<double>? SystemRadius { get; set; } = new(10.0, 100.0);
   public double DistanceMultiplier { get; set; } = 1.15;
}

