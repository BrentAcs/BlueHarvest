using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Builders;

/// <summary>
/// Fake builder, for testing, star cluster builder
/// </summary>
public class FakeStarClusterBuilder : IStarClusterBuilder
{
   public StarCluster Build(StarClusterBuilderOptions options) =>
      throw new NotImplementedException();
}
