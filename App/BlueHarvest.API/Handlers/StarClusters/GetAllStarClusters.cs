using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.API.Handlers.StarClusters;

public class GetAllStarClusters
{
   public class Request : IRequest<(IEnumerable<StarClusterResponse?>?, string?)>
   {
   }

   public class Handler: IRequestHandler<Request, (IEnumerable<StarClusterResponse?>?, string?)>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterRepo _repo;
      private readonly ILogger<Request> _logger;

      public Handler(IMapper mapper, IStarClusterRepo repo, ILogger<Request> logger)
      {
         _mapper = mapper;
         _repo = repo;
         _logger = logger;
      }

      public Task<(IEnumerable<StarClusterResponse?>?, string?)> Handle(Request request,
         CancellationToken cancellationToken)
      {
         var all = _repo.All();
         var allMapped = _mapper.Map<IEnumerable<StarClusterResponse>>(all);
         return Task.FromResult<(IEnumerable<StarClusterResponse?>?, string?)>((allMapped, null));
      }
   }
}
