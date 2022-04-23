using BlueHarvest.Shared.Models;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.PoC.CLI.Actions;

public class ListSatelliteSystemsAction : ListClusterPlaygroundAction<SatelliteSystem>
{
   public override async Task Execute()
   {
      try
      {
         ShowTitle("List Satellite Systems");

         if (!RuntimeAppState.Instance.HasCurrentPlanetarySystem)
         {
            Console.WriteLine("Dude, pick a planetary system.");
            ShowReturn();
            return;
         }

         var systems = RuntimeAppState.Instance.CurrentPlanetarySystem?.SatelliteSystems;
         int index = 1;
         var prompt = new SelectionPrompt<ListPromptItem>().PageSize(24);

         foreach (var system in systems)
         {
            prompt.AddChoice(new ListPromptItem(
               $"[white]{index++,3}[/] - [blue]{system.Name}[/]  Planet: {GetPlanetType(system.Planet.Type),-11}  Distance: {system.Distance,6:0.00}  Moons: {system.Moons.Count(),3}  Stations: {system.Stations.Count(),3}",
               system));
         }

         prompt.AddChoice(new ListPromptItem("[gray]None[/]"));
         var item = ShowPrompt(prompt);

         RuntimeAppState.Instance.CurrentSatelliteSystem = item?.Data;
      }
      catch (Exception ex)
      {
         AnsiConsole.WriteException(ex);
         ShowReturn();
      }
   }

   private static string? GetPlanetType(PlanetType planetType)
   {
      switch (planetType)
      {
         case PlanetType.Desert:
         case PlanetType.Ice:
         case PlanetType.Lava:
         case PlanetType.Oceanic:
         case PlanetType.Terrestrial:
         case PlanetType.Barren:
            return $"{planetType}";
         case PlanetType.GasGiant:
            return "Gas Giant";
         case PlanetType.IceGiant:
            return "Ice Giant";
         default:
            throw new ArgumentOutOfRangeException(nameof(planetType), planetType, null);
      }
   }
}
