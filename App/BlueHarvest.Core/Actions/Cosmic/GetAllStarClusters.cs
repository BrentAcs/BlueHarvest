using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.Actions.Cosmic;

public class GetAllStarClusters
{
   public class Request : IRequest<IEnumerable<StarClusterResponseDto?>>
   {
   }

   public class Query: BaseQuery<Request, IEnumerable<StarClusterResponseDto?>>
   {
      private readonly IStarClusterRepo _repo;

      public Query(IMediator mediator,
         IMapper mapper,
         IStarClusterRepo repo,
         ILogger<Query> logger)
         : base(mediator, mapper, logger)
      {
         _repo = repo;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<IEnumerable<StarClusterResponseDto>> OnHandle(Request request,
         CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var allMapped = Mapper.Map<IEnumerable<StarClusterResponseDto>>(all);
         return allMapped;
      }
   }
}
