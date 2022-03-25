using BlueHarvest.ConSoul.BuilderRnD;
using BlueHarvest.ConSoul.BuilderRnD.Menus;
using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Extensions;

Console.Title = "Blue Harvest ConSoul Builder RnD";

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
   Console.WindowHeight = 50;
}

MainMenu.ShowMenu();

public class StarClusterBrowser
{
   public static void Show()
   {
      Console.WriteLine("Star Cluster Browser");

#if true
      // create new star cluster

      // var options = (FakeFactory.StarClusterOptions)null;
      // var options = FakeFactory.StarClusterOptions.Empty;
      var options = new FakeFactory.StarClusterOptions {PlanetarySystemCount = 1000};
      var cluster = FakeFactory.CreateStarCluster(options);
      cluster.ToJsonFile("../../../../../SampleData/test-starcluster.json", JsonSettings.FormattedTypedNamedEnums);
#else
      // load a star cluster

      var cluster = "../../../../../SampleData/test-starcluster.json".FromJsonFile<StarCluster>();
#endif

      // Console.WriteLine($"cluster planetary count: {cluster?.PlanetarySystems.Count()}");
      // cluster.p
      Console.ReadKey(true);
   }
}
