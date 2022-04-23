using BlueHarvest.API.Actions.Cosmic;

namespace BlueHarvest.API.Tests;

public static class Utilities
{
   public static IMapper Mapper =>
      new MapperConfiguration(cfg =>
      {
         cfg.AddProfile(typeof(GetAllStarClusters.Mapping));
      }).CreateMapper();
}
