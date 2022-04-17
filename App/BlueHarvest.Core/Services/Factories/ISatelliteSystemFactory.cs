using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.Core.Services.Factories;

public interface ISatelliteSystemFactory
{
   SatelliteSystem Create(double distance);
}
