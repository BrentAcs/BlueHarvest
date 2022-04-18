using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.PoC.CLI.Menus;
using BlueHarvest.Shared.Models.Cosmic;
using static System.Console;

namespace BlueHarvest.PoC.CLI.Actions;

public class ClusterPlaygroundActions : MenuAction
{
   private readonly IStarClusterFactory _starClusterFactory;
   private readonly IStarClusterRepo _starClusterRepo;

   public ClusterPlaygroundActions(IStarClusterFactory starClusterFactory, IStarClusterRepo starClusterRepo)
   {
      _starClusterFactory = starClusterFactory;
      _starClusterRepo = starClusterRepo;
   }

   private class CreatePromptItem : PromptItem<StarClusterFactoryOptions>
   {
      public CreatePromptItem(string display, StarClusterFactoryOptions? data = default) : base(display, data)
      {
      }
   }

   public async Task CreateStarCluster()
   {
      ShowTitle("Test Star Cluster Factory");

      var prompt = new SelectionPrompt<CreatePromptItem>()
         .PageSize(20)
         .AddChoices(new[]
         {
            new CreatePromptItem("Test", StarClusterFactoryOptions.Test), new CreatePromptItem("Small", StarClusterFactoryOptions.Small),
            new CreatePromptItem("Medium", StarClusterFactoryOptions.Medium),
            new CreatePromptItem("Large", StarClusterFactoryOptions.Large),
            new CreatePromptItem("Extra Large", StarClusterFactoryOptions.ExtraLarge), new CreatePromptItem("[gray]None[/]"),
         });
      var item = AnsiConsole.Prompt(prompt);

      if (item.Data is not null)
      {
         var exists = await _starClusterRepo.ClusterExistsByNameAsync(item.Data.Name).ConfigureAwait(true);
         if (!exists)
         {
            var cluster = await _starClusterFactory.Create(item.Data).ConfigureAwait(false);
            AnsiConsole.MarkupLine($"Cluster created with id: [white]'{cluster.Id}'[/]");
         }
         else
         {
            AnsiConsole.MarkupLine($"Cluster already exists with name [yellow]{item.Data.Name}[/].");
         }
      }
      else
      {
         AnsiConsole.WriteLine($"No cluster created.");
      }

      ShowReturn();
   }

   private class ListPromptItem : PromptItem<StarCluster>
   {
      public ListPromptItem(string display, StarCluster? data = default) : base(display, data)
      {
      }
   }

   public void ListStarClusters()
   {
      ShowTitle("List Star Clusters");
      var clusters = _starClusterRepo.All();
      if (!clusters.Any())
      {
         WriteLine();
         WriteLine("No star clusters exist.");
         ShowReturn();
         return;
      }

      var prompt = new SelectionPrompt<ListPromptItem>().PageSize(20);
      foreach (var cluster in clusters)
      {
         prompt.AddChoice(new ListPromptItem($"[white]{cluster.Id}[/]: [blue]{cluster.Name}[/] {cluster.Description}", cluster));
      }
      prompt.AddChoice(new ListPromptItem("[gray]None[/]"));

      var item = AnsiConsole.Prompt(prompt);
      AppState.Cluster = item.Data;
   }
}
