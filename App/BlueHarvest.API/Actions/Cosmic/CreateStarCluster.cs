using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.API.Actions.Cosmic;

public class CreateStarCluster
{
   public class Request : IRequest<Response>
   {
      public CreateStarClusterDto Dto { get; set; }
   }

   public class Response : StarClusterDto
   {
   }

   public class Query : BaseQuery<Request, Response>
   {
      public IMapper Mapper { get; }

      public Query(ILogger<Query> logger, IMapper mapper) : base(logger)
      {
         Mapper = mapper;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<Response> OnHandle(Request request, CancellationToken cancellationToken)
      {
         var options = Mapper.Map<StarClusterFactoryOptions>(request.Dto);

         return new Response();
      }
   }

   public class Mapping : Profile
   {
      public Mapping()
      {
         CreateMap<CreateStarClusterDto, StarClusterFactoryOptions>()
            .ForMember(d => d.DesiredPlanetarySystems, o => o.MapFrom(s =>
               s.PlanetarySystemsCountExact.HasValue
                  ? new DesiredAmount(s.PlanetarySystemsCountExact.Value)
                  : s.PlanetarySystemsCountRange != null
                     ? new DesiredAmount(s.PlanetarySystemsCountRange.Min, s.PlanetarySystemsCountRange.Max)
                     : null
            ))
            .ForMember(d => d.DesiredDeepSpaceObjects, o => o.MapFrom(s =>
               s.DeepSpaceObjectsCountExact.HasValue
                  ? new DesiredAmount(s.DeepSpaceObjectsCountExact.Value)
                  : s.DeepSpaceObjectsCountRange != null
                     ? new DesiredAmount(s.DeepSpaceObjectsCountRange.Min, s.DeepSpaceObjectsCountRange.Max)
                     : null
            ))
            .ForMember(d => d.PlanetarySystemOptions, o => o.MapFrom(s =>
               s.PlanetarySystemSizeRange == null
                  ? new PlanetarySystemFactoryOptions()
                  : new PlanetarySystemFactoryOptions
                  {
                     SystemRadius = new MinMax<double>(s.PlanetarySystemSizeRange.Min, s.PlanetarySystemSizeRange.Max)
                  }))
            ;
      }
   }
}
