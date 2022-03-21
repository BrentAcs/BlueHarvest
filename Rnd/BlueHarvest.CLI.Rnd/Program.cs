using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Extensions;
using BlueHarvest.Core.Rnd.Utilities;
using Spectre.Console;
using static System.Console;

WriteLine("Blue Harvest CLI Rnd");

#if false
EntityMonikerGeneratorService.Default.Reset();
var obj = FakeFactory.CreateStarCluster();
//var obj = FakeFactory.CreatePlanetarySystem();
//var obj = FakeFactory.CreateSatelliteSystem();

var json = obj.AsJson(settings: JsonSettings.FormattedTypedNamedEnums);
obj.ToJsonFile(@"C:\Code\BlueHarvest\SampleData\star-cluster.json", JsonSettings.FormattedTypedNamedEnums);

WriteLine(json);
#endif

#if false
var cluster = @"C:\Code\BlueHarvest\SampleData\star-cluster.json".FromJsonFile<StarCluster>(JsonSettings.FormattedTypedNamedEnums);
#endif

#if true
FakeFactory.Shallow = true;
var cluster = FakeFactory.CreateStarCluster();

var col = new List<StarCluster>();
50.TimesDo(() => col.Add(FakeFactory.CreateStarCluster()));

//col.ForEach( c => WriteLine(c.Description));

var mu = new Markup("[yellow]12345  67890  12345  67890  12345  67890  12345  67890[/]").Overflow(Overflow.Crop).Alignment(Justify.Left);

var table = new Table()
      .AddColumns(
         new TableColumn("col 1").Width(30).Alignment(Justify.Center).NoWrap())
         //new TableColumn("col 2").Width(10).Alignment(Justify.Center).NoWrap())
      //.AddRow("1234567890", "1234567890")
      .AddRow(new Markup("[yellow]12345  67890  12345  67890  12345  67890  12345  67890[/]").Overflow(Overflow.Crop).Alignment(Justify.Left))
         //new Markup("[yellow]12345  67890  12345  67890  12345  67890  12345  67890[/]").Overflow(Overflow.Crop),
         //new Markup("1234567890").Overflow(Overflow.Ellipsis))
   //.AddRow("1234567890", "1234567890")
   ;

AnsiConsole.Write(table);
#endif

WriteLine("Done.");
ReadKey();

//public void ForEach(Action<T> action)

public static class IterationExtensions
{
   public static void TimesDo(this int count, Action? action)
   {
      for (int i = 0; i < count; i++)
      {
         action?.Invoke();
      }
   }
}
