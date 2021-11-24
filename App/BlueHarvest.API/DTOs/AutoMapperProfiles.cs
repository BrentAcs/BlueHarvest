using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Models.Cosmic;

namespace BlueHarvest.API.DTOs;

public class AutoMapperProfile : Profile
{
   public AutoMapperProfile()
   {
      CreateMap<CreateStarClusterRequest, StarClusterBuilderOptions>()
         .ForMember(d => d.SystemOptions, opt => opt.MapFrom(s => s.PlanetarySystemOptions));

      CreateMap<StarCluster, StarClusterResponse>();
   }
}
