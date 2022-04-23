using BlueHarvest.Core.Infrastructure.Storage.Repos;

namespace BlueHarvest.API.Actions.Cosmic;

public class GetAllStarClusters
{
   public static readonly Request Default = new();

   public class Request : IRequest<Response>
   {
   }

   public class Response
   {
      public IEnumerable<StarClusterDto?>? Dtos { get; set; }
   }

   public class Query : BaseQuery<Request, Response>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterRepo _repo;

      public Query(IMediator mediator,
         ILogger<Query> logger,
         IMapper mapper,
         IStarClusterRepo repo)
         : base(mediator, logger)
      {
         _mapper = mapper;
         _repo = repo;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<Response> OnHandle(Request request, CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var response = new Response
         {
            Dtos = _mapper.Map<IEnumerable<StarClusterDto>>(all)
         };
         return response;
      }
   }
}

public class StarClusterDto
{
}
