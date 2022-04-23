using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.PoC.CLI.Extensions;

namespace BlueHarvest.PoC.CLI.Actions;

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
