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

foreach (var item in col)
{
// public string? Name { get; set; }
// public string? Description { get; set; }
// public string? Owner { get; set; }
// public DateTime? CreatedOn { get; set; }
// public Ellipsoid? Size { get; set; }
// public List<InterstellarObject> InterstellarObjects { get; set; } = new();

   // {item.Size}

   var size = $"({item.Size.XRadius:0.00}, {item.Size.YRadius:0.00}, {item.Size.ZRadius:0.00})";
   var text = $"[green]{item.Name}[/] {item.Description} [green]{item.Owner}[/] {item.CreatedOn} [green]{size}[/] {item.InterstellarObjects.Count}";
   //WriteLine($"{item.Name} {item.Description} {item.Owner} {item.CreatedOn} {item.Size} {item.InterstellarObjects.Count}");
   
   AnsiConsole.Write( new Markup(text));
   
   WriteLine();

// table.AddRow("test data".ToMarkup(30, Color.Yellow), "boobs".ToMarkup(10));
   break;
}


// var table = new Table()
//    .AddColumns(
//       new TableColumn("col 1").Width(30).Alignment(Justify.Center).NoWrap())
//    .AddRow(new Markup("[yellow]12345  67890  12345  67890  12345  67890  12345  67890[/]").Overflow(Overflow.Crop).Alignment(Justify.Left));

// AnsiConsole.Write(table);
#endif

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
   public static void TimesDo(this int count, Action? action)
   {
      for (int i = 0; i < count; i++)
      {
         action?.Invoke();
      }
   }
}
