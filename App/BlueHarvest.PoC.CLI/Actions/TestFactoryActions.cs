using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;
using MongoDB.Bson;

namespace BlueHarvest.PoC.CLI.Actions;

internal class TestFactoryActions : MenuActions
{
   private static IRng _rng = SimpleRng.Instance;
   private readonly IMonikerGeneratorService _monikerGeneratorService = new MonikerGeneratorService();
   private readonly IStarFactory _starFactory;
   private readonly IPlanetFactory _planetFactory;
   private readonly IPlanetaryDistanceFactory _planetaryDistanceFactory;
   private readonly ISatelliteSystemFactory _satelliteSystemFactory;
   private readonly IPlanetarySystemFactory _planetarySystemFactory;
   private readonly IStarClusterFactory _starClusterFactory;

   public TestFactoryActions()
   {
      _starFactory = new StarFactory(_rng);
      _planetFactory = new PlanetFactory(_rng);
      _planetaryDistanceFactory = new PlanetaryDistanceFactory(_rng);
      _satelliteSystemFactory = new SatelliteSystemFactory(_rng, _planetFactory);
      _planetarySystemFactory = new PlanetarySystemFactory(
         _rng,
         _monikerGeneratorService,
         _starFactory,
         _planetaryDistanceFactory,
         _satelliteSystemFactory);
      _starClusterFactory = new StarClusterFactory(_rng, _planetarySystemFactory);
   }

   public void TestClusterFactory()
   {
      ShowTitle("Test Star Cluster Factory");
      var cluster = _starClusterFactory.Build(StarClusterFactoryOptions.Test);
      ShowResult(cluster);
      ShowReturn();
   }

   public void TestPlanetarySystemFactory()
   {
      ShowTitle("Test Planetary System Factory");
      var system = _planetarySystemFactory.Create(PlanetarySystemFactoryOptions.Test, ObjectId.Empty, new Point3D(42, 69, 0));
      ShowResult(system);
      ShowReturn();
   }

   public void TestSatelliteSystemFactory()
   {
      ShowTitle("Test Satellite System Factory");
      var system = _satelliteSystemFactory.Create(1.0);
      ShowResult(system);
      ShowReturn();
   }

   public void TestPlanetDistanceFactory()
   {
      ShowTitle("Test Planet Distance Factory");
      var distances = _planetaryDistanceFactory.Create(20.0);
      var data = distances.Select(d => new
      {
         Distance = d,
         Zone = d.IdentifyPlanetaryZone()
      });
      ShowResult(data);
      ShowReturn();
   }

   public void TestPlanetFactory()
   {
      ShowTitle("Test Planet Factory");
      var planet = _planetFactory.Create(1.0);
      ShowResult(planet);
      ShowReturn();
   }
}
