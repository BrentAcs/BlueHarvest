using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;

namespace BlueHarvest.Core.Builders;

public record StarClusterBuilderOptions
{
   public static StarClusterBuilderOptions ExtraLarge => new()
   {
      Name = "Extra Large",
      Description = "Work in Progress Cluster (Extra Large)",
      Owner = "System",
      ClusterSize = new Ellipsoid(150, 150, 25),
      SystemDistance = new MinMax<double>(5, 10)
   };
   
   public static StarClusterBuilderOptions Large => new()
   {
      Name = "Large",
      Description = "Work in Progress Cluster (Large)",
      Owner = "System",
      ClusterSize = new Ellipsoid(100, 100, 20),
      SystemDistance = new MinMax<double>(5, 10)
   };
   
   public static StarClusterBuilderOptions Medium => new()
   {
      Name = "Medium",
      Description = "Work in Progress Cluster (Medium)",
      Owner = "System",
      ClusterSize = new Ellipsoid(50, 50, 15),
      SystemDistance = new MinMax<double>(5, 10)
   };
   
   public static StarClusterBuilderOptions Small => new()
   {
      Name = "Small",
      Description = "Work in Progress Cluster (Small)",
      Owner = "System",
      ClusterSize = new Ellipsoid(25, 25, 10),
      SystemDistance = new MinMax<double>(5, 10)
   };
   
   public static StarClusterBuilderOptions Test => new()
   {
      Name = "Test",
      Description = "Work in Progress Cluster (Test)",
      Owner = "System",
      ClusterSize = new Ellipsoid(5, 5, 5),
      SystemDistance = new MinMax<double>(2, 5)
   };
   
   // https://www.quora.com/How-many-balls-of-diameter-1-can-be-put-in-a-spherical-container-of-diameter-10
   private const double SpherePackFactor = 0.70;
   
   public string Name { get; set; } = "(default name)";
   public string Description { get; set; } = "(default description)";
   public string Owner { get; set; } = "(default owner)";
   public Ellipsoid ClusterSize { get; set; } = new Ellipsoid(25, 25, 10);
   public MinMax<double> SystemDistance { get; set; } = new(3.0, 10.0);
   
   public PlanetarySystemBuilderOptions SystemOptions { get; set; } = new();
   
   [JsonIgnore]
   public long MaximumPossibleSystems =>
      (long)((ClusterSize.Volume / new Sphere(SystemDistance.Min).Volume) * SpherePackFactor);
}
