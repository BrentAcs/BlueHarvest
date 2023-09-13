using BlueHarvest.Shared.DTOs.Cosmic;
using BlueHarvest.Web.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace BlueHarvest.Web.Mgmt.Pages;

public partial class ListStarClusters
{
   public IEnumerable<StarClusterDto>? StarClusters { get; set; }

   [Inject]
   public IStarClusterService StarClusterService { get; set; }

   protected async override Task OnInitializedAsync()
   {
      // StarClusters = await StarClusterService.GetAll().ConfigureAwait(false);
   }
}
