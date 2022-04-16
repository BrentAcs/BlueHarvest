using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Builders;

public class PlanetarySystemBuilderOptions
{
   // 149,597,870,700 meters in an AU 

   public static PlanetarySystemBuilderOptions Test => new()
   {
      SystemRadius = new MinMax<double>(10.0, 100.0)
   };
   
   /// <summary>
   /// Min/Max radius of solar system in AU
   /// </summary>
   public MinMax<double>? SystemRadius { get; set; } = new(10.0, 100.0);
   
   //public double DistanceMultiplier { get; set; } = 1.15;
}

public interface IPlanetarySystemBuilder
{
   PlanetarySystem Build(PlanetarySystemBuilderOptions options, ObjectId clusterId, Point3D location);

}

public class PlanetarySystemBuilder : BaseBuilder, IPlanetarySystemBuilder
{
   private readonly IMonikerGeneratorService _monikerGeneratorService;

   public PlanetarySystemBuilder(IRng rng, IMonikerGeneratorService monikerGeneratorService) : base(rng)
   {
      _monikerGeneratorService = monikerGeneratorService;
   }

   public PlanetarySystem Build(PlanetarySystemBuilderOptions options, ObjectId clusterId, Point3D location)
   {
      if (options is null)
         throw new ArgumentNullException(nameof(options));
      if (options.SystemRadius is null)
         throw ArgumentPropertyNullException.Create(nameof(options), nameof(options.SystemRadius));
      
      double systemRadius = Rng.Next(options.SystemRadius);

      var system = new PlanetarySystem
      {
         Name = _monikerGeneratorService.Generate(),
         Location = location,
         Size = new Sphere(systemRadius)
      };

      return system;
   }
}
