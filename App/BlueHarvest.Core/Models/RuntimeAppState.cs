using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.Core.Models;

public class RuntimeAppState
{
   private SatelliteSystem? _currentSatelliteSystem;
   private PlanetarySystem? _currentPlanetarySystem;
   private StarCluster? _currentCluster;
   public static RuntimeAppState Instance { get; set; } = new();

   public StarCluster? CurrentCluster
   {
      get => _currentCluster;
      set
      {
         _currentCluster = value;
         _currentPlanetarySystem = null;
         _currentSatelliteSystem = null;
      } 
   }

   public PlanetarySystem? CurrentPlanetarySystem
   {
      get => _currentPlanetarySystem;
      set
      {
         _currentPlanetarySystem = value;
         _currentSatelliteSystem = null;
      }
   }

   public SatelliteSystem? CurrentSatelliteSystem
   {
      get => _currentSatelliteSystem;
      set => _currentSatelliteSystem = value;
   }

   public bool HasCurrentCluster => CurrentCluster is not null;
   public bool HasCurrentPlanetarySystem => CurrentPlanetarySystem is not null;
   public bool HasCurrentSatelliteSystem => CurrentSatelliteSystem is not null;
}

