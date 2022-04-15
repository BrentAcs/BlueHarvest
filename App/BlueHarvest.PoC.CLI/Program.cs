// See https://aka.ms/new-console-template for more information

using Spectre.Console;

do
{
   Console.Clear();
   var prompt = new SelectionPrompt<ActionPrompt>()
      .Title("Blue Harvest PoC CLI")
      .AddChoices(
         new ActionPrompt("Build Cluster", MainActions.BuildTestCluster),
         new ActionPrompt("Quit")
      );

   var item = AnsiConsole.Prompt(prompt);
   if (item.Action is null)
      break;
   item.Action?.Invoke();
} while (true);
