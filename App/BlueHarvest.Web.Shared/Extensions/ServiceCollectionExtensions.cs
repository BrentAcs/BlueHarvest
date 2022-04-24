
using BlueHarvest.Web.Shared.Services;

namespace BlueHarvest.Web.Shared.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddBlueHarvestWebShared(this IServiceCollection services)
   {
      services.AddHttpClient<IStarClusterService, StarClusterService>(client => client.BaseAddress = new Uri("https://localhost:7013/"));

      return services;
   }
}
