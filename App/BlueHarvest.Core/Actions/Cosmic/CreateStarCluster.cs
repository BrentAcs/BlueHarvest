using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Actions.Cosmic;

public class CreateStarCluster
{
   public class Request : IRequest<(StarClusterResponse?, string?)>
   {
      /// <summary>
      /// Name of the Star Cluster, must be unique.
      /// </summary>
      public string? Name { get; set; }

      public string? Description { get; set; }
      public string? Owner { get; set; }
      public Ellipsoid? ClusterSize { get; set; }
      public MinMax<double>? DistanceBetweenSystems { get; set; }

      public PlanetarySystemBuilderOptions? PlanetarySystemOptions { get; set; }
   }

   public class Handler : IRequestHandler<Request, (StarClusterResponse?, string?)>
   {
      private readonly IMediator _mediator;
      private readonly IMapper _mapper;
      private readonly ILogger<Handler> _logger;

      public Handler(IMediator mediator, IMapper mapper,
         ILogger<Handler> logger)
      {
         _mediator = mediator;
         _mapper = mapper;
         _logger = logger;
      }

      public async Task<(StarClusterResponse?, string?)> Handle(Request request,
         CancellationToken cancellationToken)
      {
         try
         {
            var clusterCreateOptions = _mapper.Map<StarClusterBuilderOptions>(request);
            var cluster = await _mediator
               .Send((StarClusterBuilder.Request)clusterCreateOptions, cancellationToken)
               .ConfigureAwait(false);
            var response = _mapper.Map<StarClusterResponse>(cluster);

            return (response, null)!;
         }
         // TODO: refactor this
         catch (MongoWriteException ex)
         {
            _logger.LogError(ex, $"Cluster w/ name '{request.Name}' already exists.");
            return (null, $"Cluster w/ name '{request.Name}' already exists.")!;
         }
      }
   }
}
