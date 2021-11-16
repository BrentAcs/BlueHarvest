using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;
using static System.Threading.Tasks.Task;

namespace BlueHarvest.Core.Builders;

public class StarClusterBuilder
{
   public class Request : IRequest<StarCluster>
   {
      public StarClusterBuilderOptions Options { get; set; }

      public static explicit operator Request(StarClusterBuilderOptions options) =>
         new() {Options = options};
   }

   public class Handler : IRequestHandler<Request, StarCluster>
   {
      private readonly IMediator _mediator;
      private readonly IStarClusterRepo _starClusterRepo;
      private readonly IRng _rng;
      private readonly IEntityDesignator _entityDesignator;

      public Handler(IMediator mediator,
         IStarClusterRepo starClusterRepo,
         IRng rng,
         IEntityDesignator entityDesignator)
      {
         _mediator = mediator;
         _starClusterRepo = starClusterRepo;
         _rng = rng;
         _entityDesignator = entityDesignator;
      }

      public async Task<StarCluster> Handle(Request request, CancellationToken cancellationToken)
      {
         var cluster = new StarCluster
         {
            CreatedOn = DateTime.Now,
            Name = request.Options.Name,
            Description = request.Options.Description,
            Owner = request.Options.Owner,
            Size = request.Options.ClusterSize
         };

         await _starClusterRepo.InsertOneAsync(cluster).ConfigureAwait(false);
         var systemLocations = GenerateSystemLocations(request.Options);

         var tasks = systemLocations.Select(location =>
            _mediator.Send(new PlanetarySystemBuilder.Request(cluster.Id, location, request.Options.SystemOptions)));
         WaitAll(tasks.ToArray(), new CancellationToken(false));

         return cluster;
      }

      private IEnumerable<Point3D> GenerateSystemLocations(StarClusterBuilderOptions options)
      {
         // NOTE: may have to check for systems to far from another
         var points = new List<Point3D>();
         for (int i = 0; i < options.MaximumPossibleSystems; i++)
         {
            bool toClose = true;
            while (toClose)
            {
               var pt = _rng.CreateRandomInside(options.ClusterSize);
               toClose = points.Any(p => p.DistanceTo(pt) < options.SystemDistance.Min);
               if (!toClose)
               {
                  points.Add(pt);
               }
            }
         }

         return points;
      }
   }
}
