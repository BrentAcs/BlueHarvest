using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.PoC.CLI.Menus;

namespace BlueHarvest.PoC.CLI.Actions;

public class ClusterPlaygroundActions : MenuActions
{
   private readonly IStarClusterFactory _starClusterFactory;
   private readonly IStarClusterRepo _starClusterRepo;

   public ClusterPlaygroundActions(IStarClusterFactory starClusterFactory, IStarClusterRepo starClusterRepo)
   {
      _starClusterFactory = starClusterFactory;
      _starClusterRepo = starClusterRepo;
   }

   public class CreatePromptItem : PromptItem<StarClusterFactoryOptions>
   {
      public CreatePromptItem(string display, StarClusterFactoryOptions? data = default) : base(display, data)
      {
      }
   }

   public async Task CreateStarCluster()
   {
      ShowTitle("Test Star Cluster Factory");
      
      var prompt = new SelectionPrompt<CreatePromptItem>()
         //.Title("Test Star Cluster Factory")
         .PageSize(20)
         .AddChoices(new[]
         {
            new CreatePromptItem("Test", StarClusterFactoryOptions.Test),
            new CreatePromptItem("Small", StarClusterFactoryOptions.Small),
            new CreatePromptItem("Medium", StarClusterFactoryOptions.Medium),
            new CreatePromptItem("Large", StarClusterFactoryOptions.Large),
            new CreatePromptItem("Extra Large", StarClusterFactoryOptions.ExtraLarge),
            new CreatePromptItem("[gray]None[/]"),
         });
      var item = AnsiConsole.Prompt(prompt);

      if (item.Data is not null)
      {
         var cluster = await _starClusterFactory.Create(item.Data).ConfigureAwait(false); 
         //ShowResult(cluster);
         AnsiConsole.WriteLine($"Cluster created with id: [/white]'{cluster.Id}'[/]");
      }
      else
      {
         AnsiConsole.WriteLine($"No cluster created.");
      }
     
      
      ShowReturn();
   }
   
   public void ListStarClusters()
   {
      ShowTitle("List Star Clusters");
      var clusters = _starClusterRepo.All();
      if (!clusters.Any())
      {
         Console.WriteLine("No star clusters exist.");         
      }
      else
      {
         int index = 0;
         foreach (var cluster in clusters)
         {
            Console.WriteLine($"{++index} - {cluster.Id} {cluster.Name}: {cluster.Description}");
         }      
      }
      ShowReturn();
   }

}
