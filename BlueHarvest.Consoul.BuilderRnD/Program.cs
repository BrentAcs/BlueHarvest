using BlueHarvest.Consoul.BuilderRnD.Menus;

Console.Title = "Blue Harvest ConSoul Builder RnD";

Console.WindowHeight = 50;

#if false
FakeFactory.Shallow = true;
var cluster = FakeFactory.CreateStarCluster();
var col = new List<StarCluster>();
50.TimesDo(() => col.Add(FakeFactory.CreateStarCluster()));

var enumerable = col as IEnumerable<StarCluster>;
//var table = col.Build( 0, 10);
var table = enumerable.Build();

AnsiConsole.Write(table);
#endif

AppMenu.Show(MainMenu.Default);

// Console.WriteLine("Done.");
// Console.ReadKey(true);

public class StarClusterBrowser
{
   public static void Show()
   {
      Console.WriteLine("Star Cluster Browser");
      Console.ReadKey(true);
   }
}
