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
50.IterationsDo(() => col.Add(FakeFactory.CreateStarCluster()));

//col.ForEach( c => WriteLine(c.Description));

var table = new Table()
   .AddColumns(
      new TableColumn("col 1").Width(30).Alignment(Justify.Center).NoWrap(),
      new TableColumn("col 2").Width(10).Alignment(Justify.Center).NoWrap());

foreach (var item in col)
{
// public string? Name { get; set; }
// public string? Description { get; set; }
// public string? Owner { get; set; }
// public DateTime? CreatedOn { get; set; }
// public Ellipsoid? Size { get; set; }
// public List<InterstellarObject> InterstellarObjects { get; set; } = new();

   WriteLine($"{item.Name} {item.Description} {item.Owner} {item.CreatedOn} {item.Size} {item.InterstellarObjects.Count}");

// table.AddRow("test data".ToMarkup(30, Color.Yellow), "boobs".ToMarkup(10));
   break;
}

// table.AddRow("test data".ToMarkup(30, Color.Yellow), "boobs".ToMarkup(10));
// 30.IterationsDo(() => table.AddRow("12345  67890  12345  67890  12345  67890  12345  67890".ToMarkup(30), "boobs".ToMarkup(10)));
// table.AddRow("12345  67890  12345  67890  12345  67890  12345  67890".ToMarkup(30, Color.Yellow), "boobs".ToMarkup(10));
// table.AddRow("12345  67890  12345  67890  12345  67890  12345  67890".ToMarkup(30, Color.Blue), "boobs".ToMarkup(10));
// table.AddRow("12345  67890  12345  67890  12345  67890  12345  67890".ToMarkup(30, overflow: Overflow.Ellipsis), "boobs".ToMarkup(10));

//AnsiConsole.Write(table);
#endif

WriteLine();
WriteLine($"{AnsiConsole.Profile.Capabilities.AsJsonIndented()}");

WriteLine("Done.");
ReadKey();

public static class SpectreConsoleExtensions
{
   public static Markup ToMarkup(this string text, int colWidth, Color? color = null, Overflow overflow = Overflow.Crop)
   {
      if (text.Length > colWidth)
      {
         text = overflow == Overflow.Ellipsis ? $"{text[ ..(colWidth - 1) ]}…" : text[ ..colWidth ];
      }

      string markupText = color.HasValue ? $"[{color.Value}]{text}[/]" : text;

      return new Markup(markupText);
   }
}


public static class IterationExtensions
{
   public static void IterationsDo(this int count, Action? action)
   {
      for (int i = 0; i < count; i++)
      {
         action?.Invoke();
      }
   }
}
