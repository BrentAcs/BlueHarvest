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

      var builder = new StarClusterBuilder(_rng);
      var cluster = builder.Build(StarClusterBuilderOptions.Test);

      ShowResult(cluster);
      ShowReturn();
   }

   public static void BuildTestPlanetarySystem()
   {
      ShowTitle("Build Test Planetary System.");

      var builder = new PlanetarySystemBuilder(_rng, _monikerGeneratorService);
      var system = builder.Build(PlanetarySystemBuilderOptions.Test, ObjectId.Empty, new Point3D(42,69,0));
      
      ShowResult(system);
      ShowReturn();
   }
}
