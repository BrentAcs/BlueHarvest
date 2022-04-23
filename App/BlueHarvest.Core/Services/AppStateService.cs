using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Services;

public class AppStateService : IAppStateService
{
   private readonly IUserAppStateRepo _userAppStateRepo;
   private readonly IStarClusterRepo _starClusterRepo;
   private readonly IPlanetarySystemRepo _planetarySystemRepo;

   private UserAppState? _userAppState;

   public AppStateService(
      IUserAppStateRepo userAppStateRepo,
      IStarClusterRepo starClusterRepo,
      IPlanetarySystemRepo planetarySystemRepo)
   {
      _userAppStateRepo = userAppStateRepo;
      _starClusterRepo = starClusterRepo;
      _planetarySystemRepo = planetarySystemRepo;
   }

   public RuntimeAppState Get()
   {
      // NOTE: For now, we have only one app-state in the db. This WILL change.
      _userAppState = _userAppStateRepo.All().FirstOrDefault() ?? new UserAppState();

      var runtimeAppState = new RuntimeAppState();
      if (_userAppState.CurrentClusterId != ObjectId.Empty)
      {
         runtimeAppState.CurrentCluster = _starClusterRepo.FindById(_userAppState.CurrentClusterId.ToString());
      }

      if (_userAppState.CurrentPlanetarySystemId != ObjectId.Empty)
      {
         runtimeAppState.CurrentPlanetarySystem = _planetarySystemRepo.FindById(_userAppState.CurrentPlanetarySystemId.ToString());
      }

      return runtimeAppState;
   }

   public async Task Update(RuntimeAppState runtimeAppState)
   {
      _userAppState.CurrentClusterId = runtimeAppState.CurrentCluster?.Id ?? ObjectId.Empty;
      _userAppState.CurrentPlanetarySystemId = runtimeAppState.CurrentPlanetarySystem?.Id ?? ObjectId.Empty;

      if (_userAppState.Id == ObjectId.Empty)
      {
         await _userAppStateRepo.InsertOneAsync(_userAppState).ConfigureAwait(false);
      }
      else
      {
         await _userAppStateRepo.ReplaceOneAsync(_userAppState).ConfigureAwait(false);
      }
   }
}
