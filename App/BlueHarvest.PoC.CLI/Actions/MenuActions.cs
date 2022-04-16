using BlueHarvest.Core.Extensions;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class MenuActions
{
   protected static void ShowTitle(string title)
   {
      Clear();
      WriteLine(title);
   }

   protected static void ShowResult<T>(T obj) =>
      WriteLine(obj.AsJsonIndented());

   protected static void ShowReturn()
   {
      WriteLine("Press any key to return.");
      ReadKey(true);
   }
}
