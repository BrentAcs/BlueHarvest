using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface IStarClusterFactory
{
   Task<bool> CanCreate(StarClusterFactoryOptions options);
   Task<StarCluster> Create(StarClusterFactoryOptions options);
}
