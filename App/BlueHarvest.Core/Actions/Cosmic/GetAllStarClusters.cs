using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.Actions.Cosmic;

public class GetAllStarClusters
{
   public static readonly Request Default = new();

   public class Request : IRequest<IEnumerable<StarClusterResponseDto?>>
   {
   }

   public class Query : BaseQuery<Request, IEnumerable<StarClusterResponseDto?>>
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

      protected override async Task<IEnumerable<StarClusterResponseDto>> OnHandle(Request request,
         CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var allMapped = _mapper.Map<IEnumerable<StarClusterResponseDto>>(all);
         return allMapped;
      }
   }
}
