using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Models;

public abstract class StellarObject
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
   public Point3D? Location { get; set; }
}
