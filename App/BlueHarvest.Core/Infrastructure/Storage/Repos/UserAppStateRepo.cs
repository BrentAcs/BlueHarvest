using BlueHarvest.Shared.Models;

namespace BlueHarvest.Core.Infrastructure.Storage.Repos;

public class UserAppStateRepo : MongoRepository<UserAppState>, IUserAppStateRepo
{
   public UserAppStateRepo(IMongoContext? mongoContext, ILogger<MongoRepository<UserAppState>> logger) : base(mongoContext, logger)
   {
   }
}
