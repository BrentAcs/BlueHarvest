using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage.Repos;
using MongoDB.Driver;

namespace BlueHarvest.API.Handlers.StarClusters;

public class CreateStarCluster
{
   public class Request : IRequest<(Response, string)>
   {
      public string? Name { get; set; }
      public string? Description { get; set; }
      public string? Owner { get; set; }
      public Ellipsoid? Size { get; set; }
   }

   public class Response
   {
      public string? Name { get; set; }
      public string? Description { get; set; }
      public string? Owner { get; set; }
      public DateTime? CreatedOn { get; set; }
      public Ellipsoid? Size { get; set; }
   }

   public class Handler : IRequestHandler<Request, (Response, string)>
   {
      private readonly IMapper _mapper;
      private readonly IStarClusterRepo _repo;

      public Handler(IMapper mapper, IStarClusterRepo repo)
      {
         _mapper = mapper;
         _repo = repo;
      }

      public async Task<(Response?, string)> Handle(Request request, CancellationToken cancellationToken)
      {
         try
         {
            var cluster = _mapper.Map<StarCluster>(request, opts =>
            {
               opts.AfterMap((_, d) =>
               {
                  d.CreatedOn = DateTime.Now;
               });
            });
            await _repo.InsertOneAsync(cluster, cancellationToken).ConfigureAwait(false);
            var response = _mapper.Map<Response>(cluster);

            return (response, null);
         }
         catch (MongoWriteException ex)
         {
            return (null, $"Cluster w/ name '{request.Name}' already exists.");
         }
      }
   }
}
