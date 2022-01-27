using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Responses.Cosmic;

namespace BlueHarvest.Core.Actions.Cosmic;

public class CreateStarCluster
{
   public class Request : IRequest<StarClusterResponseDto?>
   {
      public CreateStarClusterDto? Dto { get; set; }
   }

   public class Query : BaseQuery<Request, StarClusterResponseDto?>
   {
      private readonly IMapper _mapper;

      public Query(IMediator mediator, 
         ILogger<Query> logger,
         IMapper mapper)
         : base(mediator, logger)
      {
         _mapper = mapper;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<StarClusterResponseDto?> OnHandle(Request request,
         CancellationToken cancellationToken)
      {
         try
         {
            var clusterCreateOptions = _mapper.Map<StarClusterBuilderOptions>(request.Dto);
            var cluster = await Mediator
               .Send((StarClusterBuilder.Request)clusterCreateOptions, cancellationToken)
               .ConfigureAwait(false);
            var response = _mapper.Map<StarClusterResponseDto>(cluster);

            return response;
         }
         catch (MongoWriteException ex)
         {
            Logger.LogError(ex, $"Cluster w/ name '{request.Dto.Name}' already exists.");
            throw new CreateStarClusterException($"Cluster w/ name '{request.Dto.Name}' already exists.", ex);
         }
      }
   }
}
