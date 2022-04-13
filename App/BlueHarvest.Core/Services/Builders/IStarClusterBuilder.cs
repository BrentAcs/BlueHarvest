using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Builders;

public interface IStarClusterBuilder
{
   StarCluster Build(StarClusterBuilderOptions options);
}
