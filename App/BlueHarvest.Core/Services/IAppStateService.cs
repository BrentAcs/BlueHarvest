using BlueHarvest.Shared.Models;

namespace BlueHarvest.Core.Services;

public interface IAppStateService
{
   RuntimeAppState Get();
   Task Update(RuntimeAppState runtimeAppState);
}
