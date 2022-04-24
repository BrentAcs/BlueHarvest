using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Models.Geometry;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.DTOs;
using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.API.MappingProfiles;

public class CommonProfile : Profile
{
   public CommonProfile()
   {
      // --- Model to Dto
      CreateMap<Ellipsoid, EllipsoidDto>();
      CreateMap<StarCluster, StarClusterDto>();

      // --- Dto to Model
      CreateMap<EllipsoidDto, Ellipsoid>();
      CreateMap<MinMaxDto<int>, MinMax<int>>();
      CreateMap<MinMaxDto<double>, MinMax<double>>();
 }
}  

