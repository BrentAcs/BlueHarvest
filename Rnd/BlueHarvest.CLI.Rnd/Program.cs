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

#if false
FakeFactory.Shallow = true;
var cluster = FakeFactory.CreateStarCluster();
var col = new List<StarCluster>();
50.TimesDo(() => col.Add(FakeFactory.CreateStarCluster()));

var enumerable = col as IEnumerable<StarCluster>;
//var table = col.Build( 0, 10);
var table = enumerable.Build();

AnsiConsole.Write(table);


// var confirm = AnsiConsole.Confirm("Big boobs, best boobs.");
// WriteLine($"{confirm}");

// var favorites = AnsiConsole.Prompt(
//    new MultiSelectionPrompt<string>()
//       .PageSize(10)
//       .Title("What are your [green]favorite fruits[/]?")
//       .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
//       .InstructionsText("[grey](Press [blue][/] to toggle a fruit, [green][/] to accept)[/]")
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

// var fruit = favorites.Count == 1 ? favorites[0] : null;
// if (string.IsNullOrWhiteSpace(fruit))
// {
//    fruit = AnsiConsole.Prompt(
//       new SelectionPrompt<string>()
//          .Mode(SelectionMode.Independent)
//          .Title("Ok, but if you could only choose [green]one[/]?")
//          .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
//          .AddChoices(favorites));
// }
//
// AnsiConsole.MarkupLine("Your selected: [yellow]{0}[/]", fruit);

var fruit = AnsiConsole.Prompt(
   new SelectionPrompt<string>()
      .Title("What's your [green]favorite fruit[/]?")
      .PageSize(10)
      .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
      .AddChoices(new [] {"Apple", "Apricot", "Avocado", "Banana", "Blackcurrant", "Blueberry", "Cherry", "Cloudberry", "Cocunut"}));

// Echo the fruit back to the terminal
AnsiConsole.WriteLine($"I agree. {fruit} is tasty!");

#endif

WriteLine("Done.");
ReadKey();




// public static class IterationExtensions
// {
//    public static void TimesDo(this int count, Action? action)
//    {
//       for (int i = 0; i < count; i++)
//       {
//          action?.Invoke();
//       }
//    }
// }
