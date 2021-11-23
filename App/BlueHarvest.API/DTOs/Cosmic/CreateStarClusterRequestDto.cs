using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;

namespace BlueHarvest.API.DTOs.Cosmic;

public class CreateStarClusterRequestDto : IRequest<(StarClusterResponseDto, string)>
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public Ellipsoid? ClusterSize { get; set; }
   public MinMax<double>? SystemDistance { get; set; }

   //    // public PlanetarySystemBuilderOptions? SystemOptions { get; set; } = new();

}
