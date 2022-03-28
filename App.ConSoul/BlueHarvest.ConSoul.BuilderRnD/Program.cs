using BlueHarvest.ConSoul.BuilderRnD;
using BlueHarvest.ConSoul.BuilderRnD.Menus;
using BlueHarvest.ConSoul.BuilderRnD.Views;
using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Extensions;

Console.Title = "Blue Harvest ConSoul Builder RnD";

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
   Console.WindowHeight = 40;
}

var cluster = "../../../../../SampleData/test-starcluster.json".FromJsonFile<StarCluster>();
new StarClusterView().Show(cluster);

//MainMenu.ShowMenu();

public class StarClusterBrowser
{
   public static void Show()
   {
      //Console.WriteLine("Star Cluster Browser");

      StarCluster? cluster = null;
#if false
      // create new star cluster

      // var options = (FakeFactory.StarClusterOptions)null;
      // var options = FakeFactory.StarClusterOptions.Empty;
      var options = new FakeFactory.StarClusterOptions {PlanetarySystemCount = 100};
      cluster = FakeFactory.CreateStarCluster(options);
      cluster.ToJsonFile("../../../../../SampleData/test-starcluster.json", JsonSettings.FormattedTypedNamedEnums);
#else
      // load a star cluster

      cluster = "../../../../../SampleData/test-starcluster.json".FromJsonFile<StarCluster>();
#endif

      new StarClusterView().Show(cluster);

      Console.WriteLine("press any key to exit.");
      Console.ReadKey(true);
   }
}
