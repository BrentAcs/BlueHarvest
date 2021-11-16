using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Builders;

public class PlanetarySystemBuilder
{
   public class Request : IRequest<PlanetarySystem>
   {
      public Request(ObjectId clusterId, Point3D location, PlanetarySystemBuilderOptions options)
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
      private readonly IRng _rng;
      private readonly IStarTypeService _starTypeService;
      private readonly IPlanetDescriptorService _planetDescriptorService;
      private readonly IEntityDesignator _entityDesignator;

      public Handler(IPlanetarySystemRepo systemRepo,
         IRng rng,
         IStarTypeService starTypeService,
         IPlanetDescriptorService planetDescriptorService,
         IEntityDesignator entityDesignator)
      {
         _systemRepo = systemRepo;
         _rng = rng;
         _starTypeService = starTypeService;
         _planetDescriptorService = planetDescriptorService;
         _entityDesignator = entityDesignator;
      }

      public async Task<PlanetarySystem> Handle(Request request, CancellationToken cancellationToken)
      {
         double systemRadius = _rng.Next(request.Options.SystemRadius);

         var system = new PlanetarySystem
         {
            ClusterId = request.ClusterId,
            StarType = _starTypeService.GetRandomType(),
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
            var planetType = _planetDescriptorService.GeneratePlanetType(zone);
            var angle = _rng.Next(0.0, 360.0);
            var planetLocation = new Point3D(
               planetDistance * Math.Sin(angle.ToRadians()),
               planetDistance * Math.Cos(angle.ToRadians()));

            var planet = new Planet
            {
               Name = $"{system.Name}-{++index}",
               Distance = planetDistance,
               Location = planetLocation,
               PlanetType = planetType,
               Diameter = (int)_planetDescriptorService.GenerateDiameter(planetType)
            };
            system.StellarObjects.Add(planet);

            planetDistance += _planetDescriptorService.GenerateNextDistance(planetType) * multiplier;
            multiplier *= request.Options.DistanceMultiplier;
         }

         await _systemRepo.InsertOneAsync(system).ConfigureAwait(false);

         return system;
      }

      private double GetFirstPlanetDistance() => _rng.Next(0.3, 1.0);
   }
}

// public class PlanetarySystemBuilder : IPlanetarySystemBuilder
// {
//    private readonly ILogger<PlanetarySystemBuilder> _logger;
//    private readonly IPlanetarySystemRepo _systemRepo;
//    private readonly IRng _rng;
//    private readonly IStarTypeService _starTypeService;
//    private readonly IPlanetDescriptorService _planetDescriptorService;
//    private readonly IEntityDesignator _entityDesignator;
//
//    public PlanetarySystemBuilder(
//       ILogger<PlanetarySystemBuilder> logger,
//       IPlanetarySystemRepo systemRepo,
//       IRng rng,
//       IStarTypeService starTypeService,
//       IPlanetDescriptorService planetDescriptorService,
//       IEntityDesignator entityDesignator)
//    {
//       _logger = logger;
//       _systemRepo = systemRepo;
//       _rng = rng;
//       _starTypeService = starTypeService;
//       _planetDescriptorService = planetDescriptorService;
//       _entityDesignator = entityDesignator;
//    }
//
//    public async Task<PlanetarySystem> Create(ObjectId clusterId,
//       Point3D location,
//       PlanetarySystemBuilderOptions? options = null)
//    {
//       options ??= new PlanetarySystemBuilderOptions();
//
//       double systemRadius = _rng.Next(options.SystemRadius);
//
//       var system = new PlanetarySystem
//       {
//          ClusterId = clusterId,
//          StarType = _starTypeService.GetRandomType(),
//          Location = location,
//          Name = _entityDesignator.Generate(),
//          Size = new Sphere(systemRadius)
//       };
//
//       double planetDistance = GetFirstPlanetDistance();
//       double multiplier = 1.0;
//       int index = 0;
//
//       while (planetDistance < systemRadius)
//       {
//          var zone = _planetDescriptorService.IdentifyPlanetaryZone(planetDistance);
//          var planetType = _planetDescriptorService.GeneratePlanetType(zone);
//          var angle = _rng.Next(0.0, 360.0);
//          var planetLocation = new Point3D(
//             planetDistance * Math.Sin(angle.ToRadians()),
//             planetDistance * Math.Cos(angle.ToRadians()));
//
//          var planet = new Planet
//          {
//             Name = $"{system.Name}-{++index}",
//             Distance = planetDistance,
//             Location = planetLocation,
//             PlanetType = planetType,
//             Diameter = (int)_planetDescriptorService.GenerateDiameter(planetType)
//          };
//          system.StellarObjects.Add(planet);
//
//          planetDistance += _planetDescriptorService.GenerateNextDistance(planetType) * multiplier;
//          multiplier *= options.DistanceMultiplier;
//       }
//
//       await _systemRepo.InsertOneAsync(system).ConfigureAwait(false);
//
//       return system;
//    }
//
//    private double GetFirstPlanetDistance() => _rng.Next(0.3, 1.0);
// }
