﻿using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Models;

// [KnownType(typeof(PlanetType))]
public class Planet : StellarObject
{
   public PlanetType PlanetType { get; set; }
   public Point3D Location { get; set; }
   public int Diameter { get; set; }
}