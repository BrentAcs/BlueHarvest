using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.PoC.CLI.Extensions;
using BlueHarvest.PoC.CLI.Menus;
using BlueHarvest.Shared.Models;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.PoC.CLI.Actions;

public abstract class ListClusterPlaygroundAction<T> : ClusterPlaygroundAction
{
   protected class ListPromptItem : PromptItem<T>
   {
      public ListPromptItem(string display, T? data = default) : base(display, data)
      {
      }

      public ListPromptItem(string display, PageNav navDir) : base(display, default)
      {
         NavDir = navDir;
      }

      public bool CanNav => NavDir.HasValue;
      public PageNav? NavDir { get; }
   }

   protected static ListPromptItem ShowPrompt(SelectionPrompt<ListPromptItem> prompt)
   {
      int page = 1;
      bool done = false;
      return ShowPrompt(prompt, ref page, ref done);
   }

   protected static ListPromptItem ShowPrompt(SelectionPrompt<ListPromptItem> prompt, ref int page, ref bool done)
   {
      ListPromptItem item;
      item = AnsiConsole.Prompt(prompt);
      if (item.CanNav)
      {
         if (item.NavDir == PageNav.Previous)
            page--;
         if (item.NavDir == PageNav.Next)
            page++;
      }
      else
      {
         done = true;
      }

      return item;
   }
}

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
               $"[white]{index++,3}[/] - [blue]{system.Name}[/]  Star: {GetPlanetType(system.Planet.Type),-11}  Size: {system.Distance,6:0.00}  Moons: {system.Moons.Count(),3}  Stations: {system.Stations.Count(),3}",
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

public class ListPlanetarySystemsAction : ListClusterPlaygroundAction<PlanetarySystem>
{
   private readonly IPlanetarySystemRepo _repo;

   public ListPlanetarySystemsAction(IPlanetarySystemRepo repo)
   {
      _repo = repo;
   }

   public override async Task Execute()
   {
      ShowTitle("List Planetary Systems");
      try
      {
         if (!RuntimeAppState.Instance.HasCurrentCluster)
         {
            Console.WriteLine("Dude, pick a cluster.");
            ShowReturn();
            return;
         }

         int page = 1;
         const int pageSize = 20;
         bool done = false;
         ListPromptItem? item = null;
         while (!done)
         {
            (long total, var planetarySystems) =
               await _repo.FindAllSortByDesignationAscending(RuntimeAppState.Instance.CurrentCluster.Id, page, pageSize)
                  .ConfigureAwait(false);

            var prompt = BuildPrompt(page, planetarySystems, pageSize, total);

            item = ShowPrompt(prompt, ref page, ref done);
         }

         RuntimeAppState.Instance.CurrentPlanetarySystem = item?.Data;
      }
      catch (Exception ex)
      {
         AnsiConsole.WriteException(ex);
         ShowReturn();
      }
   }

   private static SelectionPrompt<ListPromptItem> BuildPrompt(int page, IEnumerable<PlanetarySystem> planetarySystems, int pageSize,
      long total)
   {
      var prompt = new SelectionPrompt<ListPromptItem>().PageSize(24);
      if (page > 1)
      {
         prompt.AddChoice(new ListPromptItem("Previous", PageNav.Previous));
      }

      int index = pageSize * (page - 1);
      foreach (var system in planetarySystems)
      {
         var test = system.Location.ToMarkup();
         prompt.AddChoice(new ListPromptItem(
            $"[white]{index++,3}[/] - [blue]{system.Name}[/]  Star: {system.Star.Type}  Size: {system.Size.XDiameter,6:0.00}  Planets: {system.SatelliteSystems.Count(),3}  Fields: {system.AsteroidFields.Count(),3}",
            system));
      }

      if (pageSize * page < total)
      {
         prompt.AddChoice(new ListPromptItem("Next", PageNav.Next));
      }

      prompt.AddChoice(new ListPromptItem("[gray]None[/]"));
      return prompt;
   }
}
