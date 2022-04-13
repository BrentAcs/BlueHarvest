using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Builders;

public class StarClusterBuilderOptions
{
   public static StarClusterBuilderOptions Test => new()
   {
      Name = "Test",
      Description = "Work in Progress Cluster (Test)",
      Owner = "System",
      ClusterSize = new Ellipsoid(5, 5, 5),
      DistanceBetweenSystems = new MinMax<double>(2, 5),
      SystemAmount = new SystemAmount(10)
   };
   
   // https://www.quora.com/How-many-balls-of-diameter-1-can-be-put-in-a-spherical-container-of-diameter-10
   private const double SpherePackFactor = 0.70;
   
   public string Name { get; set; } = "(default name)";
   public string Description { get; set; } = "(default description)";
   public string Owner { get; set; } = "(default owner)";
   public Ellipsoid ClusterSize { get; set; } = new(25, 25, 10);
   public MinMax<double> DistanceBetweenSystems { get; set; } = new(3.0, 10.0);
   public SystemAmount SystemAmount { get; set; }
   
   //public PlanetarySystemBuilderOptions? SystemOptions { get; set; } = new();
   
   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public long MaximumPossibleSystems =>
      (long)((ClusterSize.Volume / new Sphere(DistanceBetweenSystems.Min).Volume) * SpherePackFactor);
}
