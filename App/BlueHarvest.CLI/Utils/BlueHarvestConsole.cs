using static System.Console;

namespace BlueHarvest.CLI.Utils;

public static class BlueHarvestConsole
{
   public static void ClearScreen(string message = null)
   {
      Clear();
      if (!string.IsNullOrEmpty(message))
      {
         WriteLine(message);
      }
   }
   
   public static ConsoleKeyInfo PressAnyKey() => PromptUser("Press any key to continue");

   public static ConsoleKeyInfo PromptUser(string prompt)
   {
      Write(prompt);
      return ReadKey();
   }
}
