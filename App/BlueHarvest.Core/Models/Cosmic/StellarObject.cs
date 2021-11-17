using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Models.Cosmic;

public abstract class StellarObject : IRootModel
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
   public Point3D? Location { get; set; }
}
