using BlueHarvest.ConSoul.BuilderRnD;
using BlueHarvest.ConSoul.BuilderRnD.Menus;

Console.Title = "Blue Harvest ConSoul Builder RnD";

if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
   Console.WindowHeight = 50;
}

MainMenu.ShowMenu();

// Console.WriteLine("Done.");
// Console.ReadKey(true);

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


public class StarClusterBrowser
{
   public static void Show()
   {
      Console.WriteLine("Star Cluster Browser");
      
      var cluster = FakeFactory.CreateStarCluster();
      
      
      //File.WriteAllText("~/Code/");

      
      
      Console.ReadKey(true);
   }
}
