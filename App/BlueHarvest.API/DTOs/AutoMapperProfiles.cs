using BlueHarvest.API.Handlers.StarClusters;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.DTOs;

public class AutoMapperProfile: Profile
{
   public AutoMapperProfile()
   {
      CreateMap<CreateStarCluster.Request, StarCluster>()
         .AfterMap((_, d) => d.Description += "after-map");

      CreateMap<StarCluster, CreateStarCluster.Response>();
   }
}
