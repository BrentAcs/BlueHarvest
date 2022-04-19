using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Shared.Models;

public class RuntimeAppState
{
   public static RuntimeAppState Instance { get; set; } = new();
   
   public StarCluster? CurrentCluster { get; set; }
   public PlanetarySystem? CurrentPlanetarySystem { get; set; }
   
   public bool HasCurrentCluster => CurrentCluster is not null;
   public bool HasCurrentPlanetarySystem => CurrentPlanetarySystem is not null;
}

