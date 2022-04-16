using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Factories;

public class PlanetarySystemFactory : BaseFactory, IPlanetarySystemFactory
{
   private readonly IMonikerGeneratorService _monikerGeneratorService;
   private readonly IStarFactory _starFactory;
   private readonly IPlanetaryDistanceFactory _planetaryDistanceFactory;
   private readonly ISatelliteSystemFactory _satelliteSystemFactory;

   public PlanetarySystemFactory(IRng rng,
      IMonikerGeneratorService monikerGeneratorService,
      IStarFactory starFactory,
      IPlanetaryDistanceFactory planetaryDistanceFactory,
      ISatelliteSystemFactory satelliteSystemFactory) : base(rng)
   {
      _monikerGeneratorService = monikerGeneratorService;
      _starFactory = starFactory;
      _planetaryDistanceFactory = planetaryDistanceFactory;
      _satelliteSystemFactory = satelliteSystemFactory;
   }

   public PlanetarySystem Create(PlanetarySystemFactoryOptions options, ObjectId clusterId, Point3D location)
   {
      if (options is null)
         throw new ArgumentNullException(nameof(options));
      if (options.SystemRadius is null)
         throw ArgumentPropertyNullException.Create(nameof(options), nameof(options.SystemRadius));

      double systemRadius = Rng.Next(options.SystemRadius);

      var system = new PlanetarySystem
      {
         ClusterId = clusterId,
         Name = _monikerGeneratorService.Generate(),
         Location = location,
         Size = new Sphere(systemRadius),
         Star = _starFactory.Create()
      };

      var planetDistances = _planetaryDistanceFactory.Create(system.Size.XRadius);
      foreach (double planetDistance in planetDistances)
      {
         var satelliteSystem = _satelliteSystemFactory.Create(planetDistance);
         system.StellarObjects.Add(satelliteSystem);
      }

      return system;
   }
}

