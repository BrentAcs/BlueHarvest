using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Builders;

public interface IStarClusterBuilder
{
   Task<StarCluster> Build(StarClusterBuilderOptions options = null);
}

public class StarClusterBuilder : IStarClusterBuilder
{
   private readonly ILogger<StarClusterBuilder> _logger;
   private readonly IPlanetarySystemBuilder _planetarySystemBuilder;
   private readonly IStarClusterRepo _starClusterRepo;
   private readonly IRng _rng;
   private readonly IEntityDesignator _entityDesignator;

   public StarClusterBuilder(
      ILogger<StarClusterBuilder> logger,
      IPlanetarySystemBuilder planetarySystemBuilder,
      IStarClusterRepo starClusterRepo,
      IRng rng,
      IEntityDesignator entityDesignator)
   {
      _logger = logger;
      _planetarySystemBuilder = planetarySystemBuilder;
      _starClusterRepo = starClusterRepo;
      _rng = rng;
      _entityDesignator = entityDesignator;
   }

   public async Task<StarCluster> Build(StarClusterBuilderOptions options = null)
   {
      options ??= new StarClusterBuilderOptions();

      var cluster = new StarCluster
      {
         CreatedOn = DateTime.Now,
         Owner = options.Owner,
         Description = options.Description,
         Size = options.ClusterSize
      };
      await _starClusterRepo.InsertOneAsync(cluster).ConfigureAwait(false);

      var systemLocations = GenerateSystemLocations(options);

      foreach (var location in systemLocations)
      {
         await _planetarySystemBuilder.Create(cluster.Id, location, options.SystemOptions);
      }

      return cluster;
   }

   private IEnumerable<Point3D> GenerateSystemLocations(StarClusterBuilderOptions options)
   {
      // NOTE: may have to check for systems to far from another
      var points = new List<Point3D>();
      for (int i = 0; i < options.MaximumPossibleSystems; i++)
      {
         bool toClose = true;
         while (toClose)
         {
            var pt = _rng.CreateRandomInside(options.ClusterSize);
            toClose = points.Any(p => p.DistanceTo(pt) < options.SystemDistance.Min);
            if (!toClose)
            {
               points.Add(pt);
            }
         }
      }

      return points;
   }
}
