using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.API.Handlers.StarClusters;

public class GetStarCluster
{
   public class Request : IRequest<(StarClusterResponse?, string?)>
   {
      public Request(string? starClusterName = default)
      {
         StarClusterName = starClusterName;
      }

      public string? StarClusterName { get; set; }
   }

   public class Handler : IRequestHandler<Request, (StarClusterResponse?, string?)>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterRepo _repo;
      private readonly ILogger<GetStarCluster> _logger;

      public Handler(IMapper mapper, IStarClusterRepo repo, ILogger<GetStarCluster> logger)
      {
         _mapper = mapper;
         _repo = repo;
         _logger = logger;
      }

      public async Task<(StarClusterResponse?, string?)> Handle(Request? request,
         CancellationToken cancellationToken)
      {
         try
         {
            var cursor = await _repo.FindByNameAsync(request.StarClusterName, cancellationToken).ConfigureAwait(false);
            var cluster = await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
            if (cluster is null)
            {
               return (null, null);
            }

            var response = _mapper.Map<StarClusterResponse>(cluster);

            return (response, null);
         }
         catch (Exception? ex)
         {
            _logger.LogError(ex,
               $"Error getting star cluster for name '{request?.StarClusterName}'. Error: {ex?.Message}");
            return (null, $"Error getting star cluster for name '{request?.StarClusterName}'. Error: {ex?.Message}");
         }
      }
   }
}
