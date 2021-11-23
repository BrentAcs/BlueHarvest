using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Storage.Repos;
using MongoDB.Driver;

namespace BlueHarvest.API.Handlers.StarClusters;

public class CreateStarCluster : IRequestHandler<CreateStarClusterRequestDto, (CreateStarClusterResponseDto, string)>
{
   private readonly IMediator _mediator;
   private readonly IMapper _mapper;
   private readonly IStarClusterRepo _repo;
   private readonly ILogger<CreateStarCluster> _logger;

   public CreateStarCluster(IMediator mediator, IMapper mapper, IStarClusterRepo repo,
      ILogger<CreateStarCluster> logger)
   {
      _mediator = mediator;
      _mapper = mapper;
      _repo = repo;
      _logger = logger;
   }

   public async Task<(CreateStarClusterResponseDto, string)> Handle(CreateStarClusterRequestDto request,
      CancellationToken cancellationToken)
   {
      try
      {
         var clusterCreateOptions = _mapper.Map<StarClusterBuilderOptions>(request);
         var cluster = await _mediator.Send((StarClusterBuilder.Request)clusterCreateOptions, cancellationToken)
            .ConfigureAwait(false);
         var response = _mapper.Map<CreateStarClusterResponseDto>(cluster);

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
