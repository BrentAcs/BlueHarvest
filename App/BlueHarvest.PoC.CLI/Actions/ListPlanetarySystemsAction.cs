using System.Xml.Schema;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.PoC.CLI.Extensions;
using BlueHarvest.PoC.CLI.Menus;
using BlueHarvest.Shared.Models;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.PoC.CLI.Actions;

public class ListPlanetarySystemsAction : ClusterPlaygroundAction
{
   private readonly IPlanetarySystemRepo _repo;

   public ListPlanetarySystemsAction(IPlanetarySystemRepo repo)
   {
      _repo = repo;
   }

   private enum PageNav
   {
      Previous,
      Next,
   }

   private class ListPromptItem : PromptItem<PlanetarySystem>
   {
      public ListPromptItem(string display, PlanetarySystem? data = default) : base(display, data)
      {
      }

      public ListPromptItem(string display, PageNav navDir) : base(display, null)
      {
         NavDir = navDir;
      }

      public bool CanNav => NavDir.HasValue;
      public PageNav? NavDir { get; }
   }

   public override async Task Execute()
   {
      ShowTitle("List Star Clusters");
      try
      {
         if (!RuntimeAppState.Instance.HasCurrentCluster)
         {
            Console.WriteLine("Dude, pick a cluster.");
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

         Console.WriteLine(item);
      }
      catch (Exception ex)
      {
         AnsiConsole.WriteException(ex);
      }
      finally
      {
         ShowReturn();
      }
   }

   private static ListPromptItem ShowPrompt(SelectionPrompt<ListPromptItem> prompt, ref int page, ref bool done)
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

   private static SelectionPrompt<ListPromptItem> BuildPrompt(int page, IEnumerable<PlanetarySystem> planetarySystems, int pageSize,
      long total)
   {
      var prompt = new SelectionPrompt<ListPromptItem>().PageSize(24);
      if (page > 1)
      {
         prompt.AddChoice(new ListPromptItem("Previous", PageNav.Previous));
      }

      // public Point3D? Location { get; set; }
      // public string? Name { get; set; }
      // public Star? Star { get; set; } = new();
      // public Sphere? Size { get; set; } = new(0);
      // public List<StellarObject>? StellarObjects { get; set; } = new();
      //
      // [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
      // public IEnumerable<SatelliteSystem>? SatelliteSystems => StellarObjects?.OfType<SatelliteSystem>();
      //
      // [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
      // public IEnumerable<AsteroidField>? AsteroidFields => StellarObjects?.OfType<AsteroidField>();

      int index = pageSize * (page-1);
      foreach (var system in planetarySystems)
      {
         var test = system.Location.ToMarkup();
         prompt.AddChoice(new ListPromptItem($"[white]{index++:000}[/] - [blue]{system.Name}[/] {system.Star.Type} ", system));
      }

      if (pageSize * page < total)
      {
         prompt.AddChoice(new ListPromptItem("Next", PageNav.Next));
      }

      prompt.AddChoice(new ListPromptItem("[gray]None[/]"));
      return prompt;
   }
}
