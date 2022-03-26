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

MainMenu.ShowMenu();

public class StarClusterBrowser
{
   public static void Show()
   {
      //Console.WriteLine("Star Cluster Browser");

#if false
      // create new star cluster

      // var options = (FakeFactory.StarClusterOptions)null;
      // var options = FakeFactory.StarClusterOptions.Empty;
      var options = new FakeFactory.StarClusterOptions {PlanetarySystemCount = 100};
      var cluster = FakeFactory.CreateStarCluster(options);
      cluster.ToJsonFile("../../../../../SampleData/test-starcluster.json", JsonSettings.FormattedTypedNamedEnums);
#else
      // load a star cluster

      //var cluster = "../../../../../SampleData/test-starcluster.json".FromJsonFile<StarCluster>();
#endif
      
      new StarClusterView().Show();
      
      // var favorites = AnsiConsole.Prompt(
      //    new SelectionPrompt<string>()
      //       .PageSize(10)
      //       .Title("What are your [green]favorite fruits[/]?")
      //       //.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
      //       .AddChoiceGroup("Berries", new[]
      //       {
      //          "Blackcurrant", "Blueberry", "Cloudberry",
      //          "Elderberry", "Honeyberry", "Mulberry"
      //       })
      //       .AddChoices(new[]
      //       {
      //          "Apple", "Apricot", "Avocado", "Banana",
      //          "Cherry", "Cocunut", "Date", "Dragonfruit", "Durian",
      //          "Egg plant",  "Fig", "Grape", "Guava",
      //          "Jackfruit", "Jambul", "Kiwano", "Kiwifruit", "Lime", "Lylo",
      //          "Lychee", "Melon", "Nectarine", "Orange", "Olive"
      //       }));      

      
      // Console.WriteLine($"cluster planetary count: {cluster?.PlanetarySystems.Count()}");
      // cluster.p
      Console.WriteLine("press any key.");
      Console.ReadKey(true);
   }
}
