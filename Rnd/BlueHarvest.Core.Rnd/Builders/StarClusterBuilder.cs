using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Rnd.Models;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Rnd.Builders;

public class StarClusterBuilder
{
   private readonly IRng _rng;
   private readonly IEntityDesignator _entityDesignator;

   public StarClusterBuilder(IRng rng,
      IEntityDesignator entityDesignator)
   {
      _rng = rng;
      _entityDesignator = entityDesignator;
   }
   
   public StarCluster? Build(StarClusterBuilderOptions options)
   {
      var starCluster = new StarCluster
      {
         CreatedOn = DateTime.Now,
         Owner = options.Owner,
         Description = options.Description,
         Size = options.ClusterSize
      };

      // var systemLocations = GenerateSystemLocations(options);
      // foreach (var location in systemLocations)
      // {
      //    await _planetarySystemBuilder.Create(cluster.Id, location, options.SystemOptions);
      // }
      
      return starCluster;
   }
}
