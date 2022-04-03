using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Infastructure.Storage;

namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// Base of objects found in interstellar space.
/// Distances between these objects is measured in light-years
/// </summary>
public abstract class InterstellarObject //: ISystemModel
{
   [JsonConverter(typeof(ObjectIdConverter))]
   public ObjectId ClusterId { get; set; }
   public Point3D? Location { get; set; }
   public string? Name { get; set; }
}
