using BlueHarvest.Core.Geometry;

namespace BlueHarvest.API.DTOs.Cosmic;

public class StarClusterResponseDto
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}
