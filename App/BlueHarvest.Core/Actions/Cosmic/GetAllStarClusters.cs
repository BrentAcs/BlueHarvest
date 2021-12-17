using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.Actions.Cosmic;

public class GetAllStarClusters
{
   public class Request : IRequest<IEnumerable<StarClusterResponseDto?>>
   {
   }

   public class Handler: BaseHandler<Request, IEnumerable<StarClusterResponseDto?>>
   {
      private readonly IStarClusterRepo _repo;

      public Handler(IMediator mediator,
         IMapper mapper,
         IStarClusterRepo repo,
         ILogger<Handler> logger)
         : base(mediator, mapper, logger)
      {
         _repo = repo;
      }

      protected override string HandlerName => nameof(Handler);

      protected override async Task<IEnumerable<StarClusterResponseDto>> OnHandle(Request request,
         CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var allMapped = Mapper.Map<IEnumerable<StarClusterResponseDto>>(all);
         return allMapped;
      }
   }
}
