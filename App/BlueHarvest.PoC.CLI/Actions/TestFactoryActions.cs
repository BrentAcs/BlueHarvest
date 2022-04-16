using BlueHarvest.Core.Services;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Geometry;
using MongoDB.Bson;

namespace BlueHarvest.PoC.CLI.Actions;

internal class TestFactoryActions : MenuActions
{
   private static IRng _rng = SimpleRng.Instance;
   private static readonly IMonikerGeneratorService _monikerGeneratorService = new MonikerGeneratorService(); 
   
   public static void BuildTestCluster()
   {
      ShowTitle("Build Test Star Cluster.");

      var starFactory = new StarFactory(_rng);
      var systemBuilder = new PlanetarySystemFactory(_rng, _monikerGeneratorService, starFactory);
      var builder = new StarClusterFactory(_rng, systemBuilder);
      var cluster = builder.Build(StarClusterFactoryOptions.Large);

      ShowResult(cluster);
      ShowReturn();
   }

   public static void BuildTestPlanetarySystem()
   {
      ShowTitle("Build Test Planetary System.");

      var starFactory = new StarFactory(_rng);
      var builder = new PlanetarySystemFactory(_rng, _monikerGeneratorService, starFactory);
      var system = builder.Build(PlanetarySystemFactoryOptions.Test, ObjectId.Empty, new Point3D(42,69,0));
      
      ShowResult(system);
      ShowReturn();
   }
}
