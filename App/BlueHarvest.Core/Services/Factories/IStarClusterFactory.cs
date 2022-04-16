using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IStarClusterFactory
{
   StarCluster Build(StarClusterFactoryOptions options);
}
