﻿using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services.Factories;

public class PlanetarySystemFactoryOptions
{
   // 149,597,870,700 meters in an AU 
   public static PlanetarySystemFactoryOptions Test => new() {SystemRadius = new MinMax<double>(10.0, 100.0)};

   /// <summary>
   /// Min/Max radius of solar system in AU
   /// </summary>
   public MinMax<double>? SystemRadius { get; set; } = new(10.0, 100.0);
}
