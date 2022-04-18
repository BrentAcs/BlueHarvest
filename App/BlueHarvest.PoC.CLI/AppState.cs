using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.PoC.CLI;

public static class AppState
{
   public static StarCluster? Cluster { get; set; }

   public static bool HasCluster => Cluster is not null;
}
