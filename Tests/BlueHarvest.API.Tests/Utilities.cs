using BlueHarvest.API.Actions.Cosmic;
using BlueHarvest.API.MappingProfiles;
using BlueHarvest.Core.Infrastructure.Storage.Repos;

namespace BlueHarvest.API.Tests;

public static class Utilities
{
   public static IMapper Mapper =>
      new MapperConfiguration(cfg =>
      {
         cfg.AddMaps(typeof(CommonProfile));
      }).CreateMapper();

   public static IFixture GetSimpleFixture()
   {
      var fixture = new Fixture();
      fixture.Register(() => ObjectId.Empty);

      return fixture;
   }

   public static GetAllStarClusters.Query CreateGetAllStarClustersQuery(
      IMock<IStarClusterRepo> repoMock,
      IMock<ILogger<GetAllStarClusters.Query>>? loggerMock = null)
   {
      loggerMock ??= new Mock<ILogger<GetAllStarClusters.Query>>();

      return new GetAllStarClusters.Query(loggerMock.Object, Mapper, repoMock.Object);
   }
}
