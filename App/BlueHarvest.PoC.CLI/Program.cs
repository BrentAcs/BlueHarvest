// See https://aka.ms/new-console-template for more information

using BlueHarvest.PoC.CLI.Actions;
using Spectre.Console;

var factoryActions = new TestFactoryActions();

do
{
   Console.Clear();
   var prompt = new SelectionPrompt<ActionPrompt>()
      .Title("Blue Harvest PoC CLI")
      .AddChoiceGroup(new ActionPrompt("Test Factories"), new[]
      {
         new ActionPrompt($"Toggle Save-To-File (current: {factoryActions.SaveToFile})", factoryActions.ToggleSaveToFile), 
         new ActionPrompt("Star Cluster", factoryActions.TestClusterFactory),
         new ActionPrompt("Planetary System", factoryActions.TestPlanetarySystemFactory),
         new ActionPrompt("Planetary Distance", factoryActions.TestPlanetDistanceFactory),
         new ActionPrompt("Satellite System", factoryActions.TestSatelliteSystemFactory),
         new ActionPrompt("Planet", factoryActions.TestPlanetFactory),
      })
      .AddChoices(
         new ActionPrompt("Quit")
      );

   var item = AnsiConsole.Prompt(prompt);
   if (item.Action is null)
      break;
   item.Action?.Invoke();
} while (true);
