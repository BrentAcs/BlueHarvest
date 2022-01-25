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
      public Query(IMediator mediator, 
         IMapper mapper,
         ILogger<Query> logger)
         : base(mediator, mapper, logger)
      {
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<StarClusterResponseDto?> OnHandle(Request request,
         CancellationToken cancellationToken)
      {
         try
         {
            var clusterCreateOptions = Mapper.Map<StarClusterBuilderOptions>(request.Dto);
            var cluster = await Mediator
               .Send((StarClusterBuilder.Request)clusterCreateOptions, cancellationToken)
               .ConfigureAwait(false);
            var response = Mapper.Map<StarClusterResponseDto>(cluster);

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
