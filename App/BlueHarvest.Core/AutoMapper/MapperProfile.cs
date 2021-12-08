using BlueHarvest.Core.Actions.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Responses.Cosmic;

namespace BlueHarvest.Core.AutoMapper;

public class MapperProfile : Profile 
{
   public MapperProfile()
   {
      CreateMap<CreateStarCluster.Request, StarClusterBuilderOptions>()
         .ForMember(d => d.SystemOptions, opt => opt.MapFrom(s => s.PlanetarySystemOptions));

      CreateMap<StarCluster, StarClusterResponse>();
   }

}
