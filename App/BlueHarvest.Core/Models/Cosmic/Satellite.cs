namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// Base of objects found in satellite system
/// Distances between these objects is measured in kilometers
/// </summary>
public abstract class Satellite //: IRootModel
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
}
