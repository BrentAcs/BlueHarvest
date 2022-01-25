﻿using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Responses.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.Core.Actions.Cosmic;

public class GetStarClusterByName
{
   public class Request : IRequest<StarClusterResponseDto?>
   {
      public Request(string? starClusterName = default)
      {
         StarClusterName = starClusterName;
      }

      public string? StarClusterName { get; set; }
   }

   public class Query : BaseQuery<Request, StarClusterResponseDto?>
   {
      private readonly IStarClusterRepo _repo;

      public Query(IMediator mediator,
         IMapper mapper,
         ILogger<Query> logger,
         IStarClusterRepo repo)
         : base(mediator, mapper, logger)
      {
         _repo = repo;
      }

      protected override string HandlerName => nameof(Query);

      protected override async Task<StarClusterResponseDto?> OnHandle(Request? request, CancellationToken cancellationToken)
      {
         var cursor = await _repo.FindByNameAsync(request.StarClusterName, cancellationToken).ConfigureAwait(false);
         var cluster = await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
         if (cluster is null)
            return null;

         var response = Mapper.Map<StarClusterResponseDto>(cluster);
         return response;
      }
   }
}