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

var table = new Table()
   .AddColumns(
      new TableColumn(Text.Empty).Width(2).Alignment(Justify.Right).PadLeft(0).PadRight(0),
      new TableColumn("Name").Width(8).Alignment(Justify.Left),
      new TableColumn("Description").Width(24).Alignment(Justify.Left),
      new TableColumn("Owner").Width(20).Alignment(Justify.Left),
      new TableColumn("Created").Width(10).Alignment(Justify.Left),
      new TableColumn("Size").Width(12).Alignment(Justify.Left),
      new TableColumn("Objs").Width(4).Alignment(Justify.Right),
      new TableColumn("fluff").Width(17).Alignment(Justify.Right)
   );

int index = 1;
foreach (var item in col)
{
   table.AddRow(
      index.ToMarkup(2),
      item.Name.ToMarkup(8, Color.Yellow),
      item.Description.ToMarkup(24),
      item.Owner.ToMarkup(20),
      item.CreatedOn?.ToShortDateString().ToMarkup(10, color: Color.Yellow),
      $"({item.Size?.XRadius:0.}, {item.Size?.YRadius:0.}, {item.Size?.ZRadius:0.})".ToMarkup(12),
      item.InterstellarObjects.Count.ToMarkup(4),
      Text.Empty      
   );

   if (6 == ++index)
      break;
}

AnsiConsole.Write(table);
#endif

WriteLine("Done.");
ReadKey();

public static class SpectreConsoleExtensions
{
   public static Markup ToMarkup<T>(this T obj, int colWidth, Color? color = null, Overflow overflow = Overflow.Ellipsis)
   {
      if (obj is null)
         throw new ArgumentNullException(nameof(obj));

      string text = obj.ToString();
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
