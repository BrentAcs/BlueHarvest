using BlueHarvest.Core.Extensions;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class MenuActions
{
   protected static void ShowTitle(string title)
   {
      Clear();
      if (!string.IsNullOrEmpty(title))
      {
         WriteLine(title);
      }
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
