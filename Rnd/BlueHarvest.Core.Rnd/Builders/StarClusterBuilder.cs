using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Rnd.Models;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Rnd.Builders;

public class StarClusterBuilder
{
   private readonly IRng _rng;
   private readonly IEntityDesignator _entityDesignator;
   private readonly IPlanetarySystemBuilder _planetarySystemBuilder;

   public StarClusterBuilder(IRng rng,
      IEntityDesignator entityDesignator,
      IPlanetarySystemBuilder planetarySystemBuilder)
   {
      _rng = rng;
      _entityDesignator = entityDesignator;
      _planetarySystemBuilder = planetarySystemBuilder;
   }
   
   public RndStarCluster? Build(StarClusterBuilderOptions options)
   {
      var starCluster = new RndStarCluster
      {
         CreatedOn = DateTime.Now,
         Owner = options.Owner,
         Description = options.Description,
         Size = options.ClusterSize
      };

      var systemLocations = GenerateSystemLocations(options);
      // foreach (var location in systemLocations)
      // {
      //    await _planetarySystemBuilder.Create(cluster.Id, location, options.SystemOptions);
      // }
      
      return starCluster;
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
