using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Shared.Models.Cosmic;

/// <summary>
/// Base of objects found in stellar space.
/// Distances between these objects is measured in astronautical units
/// </summary>
public abstract class StellarObject //: IRootModel
{
   public string? Name { get; set; }
   public Point3D? Location { get; set; }
   //public double? Distance { get; set; }
}
