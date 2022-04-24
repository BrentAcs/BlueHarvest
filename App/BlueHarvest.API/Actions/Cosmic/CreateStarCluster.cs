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

   public class Response
   {
      public StarClusterDto Dto { get; set; }
   }

   public class Query : BaseQuery<Request, Response>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterFactory _starClusterFactory;

      public Query(ILogger<Query> logger, IMapper mapper, IStarClusterFactory starClusterFactory) : base(logger)
      {
         _mapper = mapper;
         _starClusterFactory = starClusterFactory;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<Response> OnHandle(Request request, CancellationToken cancellationToken)
      {
         var options = _mapper.Map<StarClusterFactoryOptions>(request.Dto);
         if (!await _starClusterFactory.CanCreate(options).ConfigureAwait(true))
            return null;

         var cluster = await _starClusterFactory.Create(options).ConfigureAwait(true);
         var response = new Response {Dto = _mapper.Map<StarClusterDto>(cluster)};

         return response;
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
