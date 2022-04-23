using BlueHarvest.Core.Models;

namespace BlueHarvest.Core.Infrastructure.Storage.Repos;

public interface IUserAppStateRepo : IMongoRepository<UserAppState>
{
}
