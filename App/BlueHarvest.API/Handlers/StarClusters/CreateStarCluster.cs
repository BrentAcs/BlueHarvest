﻿using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.Core.Builders;

namespace BlueHarvest.API.Handlers.StarClusters;

public class CreateStarCluster : IRequestHandler<CreateStarClusterRequest, (StarClusterResponse?, string?)>
{
   private readonly IMediator _mediator;
   private readonly IMapper _mapper;
   private readonly ILogger<CreateStarCluster> _logger;

   public CreateStarCluster(IMediator mediator, IMapper mapper, ILogger<CreateStarCluster> logger)
   {
      _mediator = mediator;
      _mapper = mapper;
      _logger = logger;
   }

   public async Task<(StarClusterResponse?, string?)> Handle(CreateStarClusterRequest request,
      CancellationToken cancellationToken)
   {
      try
      {
         var clusterCreateOptions = _mapper.Map<StarClusterBuilderOptions>(request);
         var cluster = await _mediator.Send((StarClusterBuilder.Request)clusterCreateOptions, cancellationToken)
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
