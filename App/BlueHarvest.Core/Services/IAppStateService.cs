using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Services;

public interface IAppStateService
{
   RuntimeAppState Get();
   Task Update(RuntimeAppState runtimeAppState);
}
