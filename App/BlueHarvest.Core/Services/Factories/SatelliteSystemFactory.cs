using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Factories;

public class SatelliteSystemFactory : BaseFactory, ISatelliteSystemFactory
{
   private readonly IPlanetFactory _planetFactory;

   public SatelliteSystemFactory(IRng rng, IPlanetFactory planetFactory) : base(rng)
   {
      _planetFactory = planetFactory;
   }

   public SatelliteSystem Create(double distance)
   {
      var system = new SatelliteSystem
      {
         Distance = distance,
         // TODO: need to figure out how to do this base on distance.
         Location = new Point3D(),
         Planet = _planetFactory.Create(distance)
      };

      return system;
   }
}
