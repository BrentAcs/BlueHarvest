using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IStarClusterFactory
{
   Task<StarCluster> Create(StarClusterFactoryOptions options);
}
