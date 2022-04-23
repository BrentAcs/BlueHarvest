using BlueHarvest.Core.Extensions;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class MenuAction
{
   protected static void ShowTitle(string title)
   {
      Clear();
      if (string.IsNullOrEmpty(title))
      {
         return;
      }

      AnsiConsole.Write(new Rule($"[blue]{title}[/]").RuleStyle("grey").LeftAligned());
      WriteLine();
   }

   protected static void ShowResult<T>(T obj, JsonSerializerSettings settings = null)
   {
      settings ??= JsonSettings.FormattedNamedEnums;
      WriteLine(obj.AsJson(settings));
   }

   protected static void ShowReturn()
   {
      WriteLine("Press any key to return.");
      ReadKey(true);
   }
}
