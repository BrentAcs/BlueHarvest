using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Responses.Cosmic;

public class StarClusterResponse
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}
