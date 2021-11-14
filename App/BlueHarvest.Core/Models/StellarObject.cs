using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

public abstract class StellarObject : IRootModel
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
   public Point3D? Location { get; set; }
}
