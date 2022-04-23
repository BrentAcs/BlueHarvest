using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.PoC.CLI.Menus;
using BlueHarvest.Shared.Models;
using BlueHarvest.Shared.Models.Cosmic;

namespace BlueHarvest.PoC.CLI.Actions;

public class ListStarClustersAction : ClusterPlaygroundAction
{
   private readonly IStarClusterFactory _starClusterFactory;
   private readonly IStarClusterRepo _starClusterRepo;

   public ListStarClustersAction(IStarClusterFactory starClusterFactory, IStarClusterRepo starClusterRepo)
   {
      _starClusterFactory = starClusterFactory;
      _starClusterRepo = starClusterRepo;
   }

   private class ListPromptItem : PromptItem<StarCluster>
   {
      public ListPromptItem(string display, StarCluster? data = default) : base(display, data)
      {
      }
   }

   public override Task Execute()
   {
      ShowTitle("List Star Clusters");
      var clusters = _starClusterRepo.All();
      if (!clusters.Any())
      {
         Console.WriteLine();
         Console.WriteLine("No star clusters exist.");
         ShowReturn();
         return Task.CompletedTask;
      }

      var prompt = new SelectionPrompt<ListPromptItem>().PageSize(20);
      foreach (var cluster in clusters)
      {
         prompt.AddChoice(new ListPromptItem($"[white]{cluster.Id}[/]: [blue]{cluster.Name}[/] {cluster.Description}", cluster));
      }
      prompt.AddChoice(new ListPromptItem("[gray]None[/]"));

      var item = AnsiConsole.Prompt(prompt);
      RuntimeAppState.Instance.CurrentCluster = item.Data;
      RuntimeAppState.Instance.CurrentPlanetarySystem = null;
      return Task.CompletedTask;
   }
}
