using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services.Factories;

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

   public async Task CreateStarCluster()
   {
      ShowTitle("Test Star Cluster Factory");
      
      //StarClusterFactoryOptions.Test
      //StarClusterFactoryOptions.Small
      //StarClusterFactoryOptions.Medium
      //StarClusterFactoryOptions.Large
      //StarClusterFactoryOptions.ExtraLarge

      var prompt = new SelectionPrompt<StarClusterFactoryOptions>()
         .Title("[blue]Blue Harvest[/] PoC CLI")
         .PageSize(20)
         .AddChoices(new[]
         {
            StarClusterFactoryOptions.Test,
            StarClusterFactoryOptions.Small,
            StarClusterFactoryOptions.Medium,
            StarClusterFactoryOptions.Large,
            StarClusterFactoryOptions.ExtraLarge
         });
      var item = AnsiConsole.Prompt(prompt);
     
      
      // var cluster = await _starClusterFactory.Create(StarClusterFactoryOptions.Test).ConfigureAwait(false); 
      // ShowResult(cluster);
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
