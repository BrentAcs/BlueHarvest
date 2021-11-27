using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;

namespace BlueHarvest.API.DTOs.Cosmic;

public class CreateStarClusterRequest : IRequest<(StarClusterResponse?, string?)>
{
   /// <summary>
   /// Name of the Star Cluster, must be unique.
   /// </summary>
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public Ellipsoid? ClusterSize { get; set; }
   public MinMax<double>? DistanceBetweenSystems { get; set; }

   public PlanetarySystemBuilderOptions? PlanetarySystemOptions { get; set; }
}

