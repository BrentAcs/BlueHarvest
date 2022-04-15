using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Services.Builders;
using BlueHarvest.Core.Utilities;

public static class MainActions
{
   public static void BuildTestCluster()
   {
      Console.Clear();
      Console.WriteLine("build a cluster.");

      var builder = new StarClusterBuilder(SimpleRng.Instance);
      var cluster = builder.Build(StarClusterBuilderOptions.Test);

      Console.Write(cluster.AsJsonIndented());
      Console.ReadKey(true);
   }
}
