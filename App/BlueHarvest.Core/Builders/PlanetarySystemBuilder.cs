using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Builders;

public class PlanetarySystemBuilder
{
   public class Request : IRequest<PlanetarySystem>
   {
      public Request(ObjectId clusterId, Point3D location, PlanetarySystemBuilderOptions? options)
      {
         ClusterId = clusterId;
         Location = location;
         Options = options;
      }

      public ObjectId ClusterId { get; set; }
      public Point3D? Location { get; set; }
      public PlanetarySystemBuilderOptions? Options { get; set; }
   }

   public class Handler : IRequestHandler<Request, PlanetarySystem>
   {
      private readonly IPlanetarySystemRepo _systemRepo;
      private readonly IMediator _mediator;

      private readonly IRng _rng;
      private readonly IPlanetDescriptorService _planetDescriptorService;
      private readonly IEntityDesignator _entityDesignator;

      public Handler(IPlanetarySystemRepo systemRepo,
         IMediator mediator,
         IRng rng,
         IPlanetDescriptorService planetDescriptorService,
         IEntityDesignator entityDesignator)
      {
         _systemRepo = systemRepo;
         _mediator = mediator;
         _rng = rng;
         _planetDescriptorService = planetDescriptorService;
         _entityDesignator = entityDesignator;
      }

      public async Task<PlanetarySystem> Handle(Request request, CancellationToken cancellationToken)
      {
         double systemRadius = _rng.Next(request.Options?.SystemRadius!);

         var starDescriptor = await _mediator.Send(new StarDescriptorService.RequestRandom(), cancellationToken);

         var system = new PlanetarySystem
         {
            ClusterId = request.ClusterId,
            StarType = starDescriptor.StarType,
            StarMass = _rng.Next(starDescriptor.MassRange),
            Location = request.Location,
            Name = _entityDesignator.Generate(),
            Size = new Sphere(systemRadius)
         };

         double planetDistance = GetFirstPlanetDistance();
         double multiplier = 1.0;
         int index = 0;

         while (planetDistance < systemRadius)
         {
            var zone = _planetDescriptorService.IdentifyPlanetaryZone(planetDistance);
            var planetDescriptor = _planetDescriptorService.GetRandomPlanetDescriptor(zone);
            var angle = _rng.Next(0.0, 360.0);
            var planetLocation = new Point3D(
               planetDistance * Math.Sin(angle.ToRadians()),
               planetDistance * Math.Cos(angle.ToRadians()));

            var planet = new Planet
            {
               Name = $"{system.Name}-{++index}",
               Distance = planetDistance,
               Location = planetLocation,
               PlanetType = planetDescriptor.PlanetType,
               Diameter = (int)GenerateDiameter(planetDescriptor)
            };
            system.StellarObjects!.Add(planet);

            planetDistance += GenerateNextDistance(planetDescriptor) * multiplier;
            multiplier *= request.Options!.DistanceMultiplier;
         }

         await _systemRepo.InsertOneAsync(system).ConfigureAwait(false);

         return system;
      }

      private double GetFirstPlanetDistance() => _rng.Next(0.3, 1.0);

      private double GenerateDiameter(PlanetDescriptor descriptor) =>
         _rng.Next(descriptor.Radius!) * 2;

      private double GenerateNextDistance(PlanetDescriptor descriptor) =>
         _rng.Next(descriptor.Distance!);
   }
}
