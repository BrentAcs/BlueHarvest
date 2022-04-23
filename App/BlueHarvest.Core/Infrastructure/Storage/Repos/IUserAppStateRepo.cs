using BlueHarvest.Shared.Models;

namespace BlueHarvest.Core.Infrastructure.Storage.Repos;

public interface IUserAppStateRepo : IMongoRepository<UserAppState>
{
}
