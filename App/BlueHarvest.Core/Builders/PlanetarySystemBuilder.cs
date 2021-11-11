using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Builders;

public interface IPlanetarySystemBuilder
{
   Task<PlanetarySystem> Create(ObjectId clusterId,
      Point3D location,
      PlanetarySystemBuilderOptions options = null);
}

public class PlanetarySystemBuilder : IPlanetarySystemBuilder
{
   private record PlanetDescriptor
   {
      public PlanetaryZone[] Zones { get; set; }
      public MinMax<int> Radius { get; set; }
      public MinMax<double> Distance { get; set; }
   }

   private readonly IDictionary<PlanetType, PlanetDescriptor> _planetDescriptors =
      new Dictionary<PlanetType, PlanetDescriptor>
      {
         {
            PlanetType.Desert,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.GasGiant,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Outer },
               Radius = new MinMax<int>(35000, 800000),
               Distance = new MinMax<double>(4.0, 15.0)
            }
         },
         {
            PlanetType.IceGiant,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Outer },
               Radius = new MinMax<int>(10000, 40000),
               Distance = new MinMax<double>(4.0, 15.0)
            }
         },
         {
            PlanetType.Ice,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable },
               Radius = new MinMax<int>(2000, 5000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.Lava,
            new PlanetDescriptor
            {
               Zones = new[] { PlanetaryZone.Inner, PlanetaryZone.InnerHabitable },
               Radius = new MinMax<int>(1500, 6000),
               Distance = new MinMax<double>(0.2, 0.6)
            }
         },
         {
            PlanetType.Oceanic,
            new PlanetDescriptor
            {
               Zones = new[]
               {
                  PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable
               },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
         {
            PlanetType.Terrestrial,
            new PlanetDescriptor
            {
               Zones = new[]
               {
                  PlanetaryZone.InnerHabitable, PlanetaryZone.Habitable, PlanetaryZone.OuterHabitable
               },
               Radius = new MinMax<int>(2000, 8000),
               Distance = new MinMax<double>(0.3, 0.7)
            }
         },
      };

   private readonly IDictionary<PlanetaryZone, MinMax<double>> _planetaryZoneRanges =
      new Dictionary<PlanetaryZone, MinMax<double>>
      {
         { PlanetaryZone.Inner, new MinMax<double>(0, 0.2499) },
         { PlanetaryZone.InnerHabitable, new MinMax<double>(0.25, 1.4999) },
         { PlanetaryZone.Habitable, new MinMax<double>(1.5, 3.4999) },
         { PlanetaryZone.OuterHabitable, new MinMax<double>(3.5, 4.9999) },
         { PlanetaryZone.Outer, new MinMax<double>(5.0, double.MaxValue) },
      };

   private readonly ILogger<PlanetarySystemBuilder> _logger;
   private readonly IPlanetarySystemRepo _systemRepo;
   private readonly IRng _rng;
   private readonly IStarTypeService _starTypeService;
   private readonly IEntityDesignator _entityDesignator;

   public PlanetarySystemBuilder(
      ILogger<PlanetarySystemBuilder> logger,
      IPlanetarySystemRepo systemRepo,
      IRng rng,
      IStarTypeService starTypeService,
      IEntityDesignator entityDesignator)
   {
      _logger = logger;
      _systemRepo = systemRepo;
      _rng = rng;
      _starTypeService = starTypeService;
      _entityDesignator = entityDesignator;
   }

   public async Task<PlanetarySystem> Create(ObjectId clusterId,
      Point3D location,
      PlanetarySystemBuilderOptions options = null)
   {
      double systemRadius = _rng.Next(options.SystemRadius);

      var system = new PlanetarySystem
      {
         ClusterId = clusterId,
         StarType = _starTypeService.GetRandomType(),
         Location = location,
         Name = _entityDesignator.Generate(),
         Size = new Sphere(systemRadius)
      };

      var planetDistance = GetFirstPlanetDistance();
      var multiplier = 1.0;

      while (planetDistance < systemRadius)
      {
         var zone = IdentifyPlanetaryZone(planetDistance);
         var planetType = GetPlanetType(zone);
         var descriptor = _planetDescriptors[ planetType ];
         var angle = _rng.Next(0.0, 360.0);
         var planetLocation = new Point3D(
            planetDistance * Math.Sin(angle.ToRadians()),
            planetDistance * Math.Cos(angle.ToRadians()));

         var planet = new Planet
         {
            Name = $"{system.Name}-{1}",
            Location = planetLocation,
            PlanetType = planetType,
            Diameter = _rng.Next(descriptor.Radius) * 2
         };
         system.Objects.Add(planet);


         planetDistance += _rng.Next(descriptor.Distance) * multiplier;
         multiplier *= options.DistanceMultiplier;
      }

      await _systemRepo.InsertOneAsync(system).ConfigureAwait(false);

      return system;
   }

   private double GetFirstPlanetDistance() => _rng.Next(0.3, 1.0);

   private PlanetaryZone IdentifyPlanetaryZone(double distance) =>
      _planetaryZoneRanges
         .First(p => distance > p.Value.Min && distance < p.Value.Max).Key;

   private PlanetType GetPlanetType(PlanetaryZone zone)
   {
      var planetTypes = _planetDescriptors
         .Where(p => p.Value.Zones.Contains(zone))
         .Select(p => p.Key)
         .ToList();

      return planetTypes[ _rng.Next(0, planetTypes.Count - 1) ];
   }
}
