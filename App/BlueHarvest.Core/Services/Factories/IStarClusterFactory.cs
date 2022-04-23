using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IStarClusterFactory
{
   Task<StarCluster> Create(StarClusterFactoryOptions options);
}
