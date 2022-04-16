using BlueHarvest.Core.Services;
using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Geometry;
using MongoDB.Bson;

namespace BlueHarvest.PoC.CLI.Actions;

internal class TestBuilderActions : MenuActions
{
   private static IRng _rng = SimpleRng.Instance;
   private static readonly IMonikerGeneratorService _monikerGeneratorService = new MonikerGeneratorService(); 
   
   public static void BuildTestCluster()
   {
      ShowTitle("Build Test Star Cluster.");

      var starFactory = new StarFactory(_rng);
      var systemBuilder = new PlanetarySystemBuilder(_rng, _monikerGeneratorService, starFactory);
      var builder = new StarClusterBuilder(_rng, systemBuilder);
      var cluster = builder.Build(StarClusterBuilderOptions.Large);

      ShowResult(cluster);
      ShowReturn();
   }

   public static void BuildTestPlanetarySystem()
   {
      ShowTitle("Build Test Planetary System.");

      var starFactory = new StarFactory(_rng);
      var builder = new PlanetarySystemBuilder(_rng, _monikerGeneratorService, starFactory);
      var system = builder.Build(PlanetarySystemBuilderOptions.Test, ObjectId.Empty, new Point3D(42,69,0));
      
      ShowResult(system);
      ShowReturn();
   }
}
