using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Rnd.Builders;

public interface IPlanetarySystemBuilder
{
   // RndPlanetarySystem Build(Point3D location, PlanetarySystemBuilderOptions options);
}

public class PlanetarySystemBuilder : IPlanetarySystemBuilder
{
   // private readonly IRng _rng;
   // private readonly IStarTypeService _starTypeService;
   // private readonly IPlanetDescriptorService _planetDescriptorService;
   // private readonly IEntityDesignator _entityDesignator;
   //
   // public PlanetarySystemBuilder(
   //    IRng rng,
   //    IStarTypeService starTypeService,
   //    IPlanetDescriptorService planetDescriptorService,
   //    IEntityDesignator entityDesignator)
   // {
   //    _rng = rng;
   //    _starTypeService = starTypeService;
   //    _planetDescriptorService = planetDescriptorService;
   //    _entityDesignator = entityDesignator;
   // }
   //
   // public RndPlanetarySystem Build(Point3D location, PlanetarySystemBuilderOptions options)
   // {
   //    double systemRadius = _rng.Next(options.SystemRadius);
   //
   //    var system = new RndPlanetarySystem
   //    {
   //       //ClusterId = clusterId,
   //       StarType = _starTypeService.GetRandomType(),
   //       Location = location,
   //       Name = _entityDesignator.Generate(),
   //       Size = new Sphere(systemRadius)
   //    };
   //
   //    var planetDistance = GetFirstPlanetDistance();
   //    var multiplier = 1.0;
   //
   //    while (planetDistance < systemRadius)
   //    {
   //       var zone = _planetDescriptorService.IdentifyPlanetaryZone(planetDistance);
   //       var planetType = _planetDescriptorService.GeneratePlanetType(zone);
   //       //var descriptor = _planetDescriptors[ planetType ];
   //       var angle = _rng.Next(0.0, 360.0);
   //       var planetLocation = new Point3D(
   //          planetDistance * Math.Sin(angle.ToRadians()),
   //          planetDistance * Math.Cos(angle.ToRadians()));
   //
   //       var planet = new RndPlanet
   //       {
   //          Name = $"{system.Name}-{1}",
   //          Location = planetLocation,
   //          PlanetType = planetType,
   //          Diameter = (int)_planetDescriptorService.GenerateDiameter(planetType)
   //       };
   //       system.Objects.Add(planet);
   //
   //       planetDistance += _planetDescriptorService.GenerateNextDistance(planetType) * multiplier;
   //       multiplier *= options.DistanceMultiplier;
   //    }
   //
   //    // await _systemRepo.InsertOneAsync(system).ConfigureAwait(false);
   //
   //    return system;
   // }
   //
   // private double GetFirstPlanetDistance() => _rng.Next(0.3, 1.0);
}
