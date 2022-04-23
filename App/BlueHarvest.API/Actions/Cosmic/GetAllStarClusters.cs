using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Models.Geometry;
using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.API.Actions.Cosmic;

public class GetAllStarClusters
{
   public static readonly Request Default = new();

   public class Request : IRequest<Response>
   {
   }

   public class Response
   {
      public IEnumerable<StarClusterDto?>? Data { get; set; }
   }

   public class Query : BaseQuery<Request, Response>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterRepo _repo;

      public Query(ILogger<Query> logger,
         IMapper mapper,
         IStarClusterRepo repo)
         : base(logger)
      {
         _mapper = mapper;
         _repo = repo;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<Response> OnHandle(Request request, CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var response = new Response {Data = _mapper.Map<IEnumerable<StarClusterDto>>(all)};
         return response;
      }
   }

   public class Mapping : Profile
   {
      public Mapping()
      {
         CreateMap<Ellipsoid, EllipsoidDto>();

         CreateMap<StarCluster, StarClusterDto>();
      }
   }
}
