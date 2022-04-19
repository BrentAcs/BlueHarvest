using System.Xml.Schema;
using BlueHarvest.Core.Infrastructure.Storage.Repos;

namespace BlueHarvest.PoC.CLI.Actions;

public class ListPlanetarySystemsAction : ClusterPlaygroundAction
{
   private readonly IPlanetarySystemRepo _repo;

   public ListPlanetarySystemsAction(IPlanetarySystemRepo repo)
   {
      _repo = repo;
   }
   
   public override async Task Execute()
   {
      ShowTitle("List Star Clusters");
      try
      {
         if (!AppState.HasCluster)
         {
            Console.WriteLine("Dude, pick a cluster.");
            return;
         }

         int page = 1;
         int pageSize = 20;

         var (total, data) = await _repo.FindAllSortByDesignationAscending(AppState.Cluster.Id, page, pageSize).ConfigureAwait(false);

         Console.WriteLine("We HAZ cluster.");
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
}
