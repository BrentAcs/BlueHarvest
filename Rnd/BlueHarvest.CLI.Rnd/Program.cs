using BlueHarvest.CLI.Rnd;
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

var table = StarClusterTableRenderer.Render(col, 0, 10);
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
