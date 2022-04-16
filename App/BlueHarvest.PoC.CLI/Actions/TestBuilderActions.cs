using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.PoC.CLI.Actions;

internal class TestBuilderActions : MenuActions
{
   public static void BuildTestCluster()
   {
      ShowTitle("Build Test Star Cluster.");

      var builder = new StarClusterBuilder(SimpleRng.Instance);
      var cluster = builder.Build(StarClusterBuilderOptions.Test);

      ShowResult(cluster);
      ShowReturn();
   }

   public static void BuildTestPlanetarySystem()
   {
      ShowTitle("Build Test Planetary System.");

      // todo: the needful
      
      ShowResult(new object());
      ShowReturn();
   }
}
