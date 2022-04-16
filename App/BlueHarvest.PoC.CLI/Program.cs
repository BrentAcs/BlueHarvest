// See https://aka.ms/new-console-template for more information

using BlueHarvest.PoC.CLI;
using BlueHarvest.PoC.CLI.Actions;
using Spectre.Console;

do
{
   Console.Clear();
   var prompt = new SelectionPrompt<ActionPrompt>()
      .Title("Blue Harvest PoC CLI")
      .AddChoiceGroup(new ActionPrompt("Test Builders"), new[]
      {
         new ActionPrompt("Star Cluster", TestBuilderActions.BuildTestCluster),
         new ActionPrompt("Planetary System", TestBuilderActions.BuildTestPlanetarySystem),
      })
      .AddChoices(
         new ActionPrompt("Quit")
      );

   var item = AnsiConsole.Prompt(prompt);
   if (item.Action is null)
      break;
   item.Action?.Invoke();
} while (true);
