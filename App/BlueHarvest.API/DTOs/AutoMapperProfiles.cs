using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.DTOs;

public class AutoMapperProfile : Profile
{
   public AutoMapperProfile()
   {
      CreateMap<CreateStarClusterRequestDto, StarClusterBuilderOptions>();
      CreateMap<StarCluster, StarClusterResponseDto>();
   }
}
