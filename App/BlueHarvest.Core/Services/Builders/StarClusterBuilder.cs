using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Builders;

public interface IStarClusterBuilderService
{
   
}

public class StarClusterBuilderService : IStarClusterBuilderService
{
   
}

public class StarClusterBuilderOptions
{
   public static StarClusterBuilderOptions Test => new()
   {
      Name = "Test",
      Description = "Work in Progress Cluster (Test)",
      Owner = "System",
      ClusterSize = new Ellipsoid(5, 5, 5),
      DistanceBetweenSystems = new MinMax<double>(2, 5)
   };
   
   // https://www.quora.com/How-many-balls-of-diameter-1-can-be-put-in-a-spherical-container-of-diameter-10
   private const double SpherePackFactor = 0.70;
   
   public string Name { get; set; } = "(default name)";
   public string Description { get; set; } = "(default description)";
   public string Owner { get; set; } = "(default owner)";
   public Ellipsoid ClusterSize { get; set; } = new(25, 25, 10);
   public MinMax<double> DistanceBetweenSystems { get; set; } = new(3.0, 10.0);
   
   //public PlanetarySystemBuilderOptions? SystemOptions { get; set; } = new();
   
   [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   public long MaximumPossibleSystems =>
      (long)((ClusterSize.Volume / new Sphere(DistanceBetweenSystems.Min).Volume) * SpherePackFactor);
}

public interface IStarClusterBuilder
{
   StarCluster Build(StarClusterBuilderOptions options);
}

/// <summary>
/// Stock, for game play, star cluster builder
/// </summary>
public class StarClusterBuilder : IStarClusterBuilder
{
   public StarCluster Build(StarClusterBuilderOptions options)
   {
      var cluster = new StarCluster
      {
         CreatedOn = DateTime.Now,
         Name = options.Name,
         Description = options.Description,
         Owner = options.Owner,
         Size = options.ClusterSize
      };

      return cluster;
   }
}

/// <summary>
/// Fake builder, for testing, star cluster builder
/// </summary>
public class FakeStarClusterBuilder : IStarClusterBuilder
{
   public StarCluster Build(StarClusterBuilderOptions options) =>
      throw new NotImplementedException();
}
