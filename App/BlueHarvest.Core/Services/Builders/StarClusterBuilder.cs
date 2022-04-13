using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Builders;

/// <summary>
/// Stock, for game play, star cluster builder
/// </summary>
public class StarClusterBuilder : IStarClusterBuilder
{
   private readonly IRng _rng;

   public StarClusterBuilder(IRng rng)
   {
      _rng = rng;
   }

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

      int systemCount = options.SystemAmount.GetAmount(_rng);
      if (systemCount > options.MaximumPossibleSystems)
         BuilderException.CreateTooManySystems(options.MaximumPossibleSystems, systemCount);

      return cluster;
   }
}
;
