using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddBlueHarvestCommon(this IServiceCollection services, IConfiguration configuration)
   {
      services
          .Configure<MongoDbSettings>(configuration.GetSection("MongoDb"))
          .AddSingleton<IMongoDbSettings>(serviceProvider =>
             serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
          .AddSingleton<IMongoContext, MongoContext>()
          .AddScoped<IClusterRepo, ClusterRepo>()
         // .AddScoped<IPlanetarySystemRepo, PlanetarySystemRepo>()

         .AddSingleton<IEntityDesignator, EntityDesignator>()
         .AddSingleton<IRng, SimpleRng>()
         // .AddSingleton<IStarTypeService, StarTypeService>()
         // .AddSingleton<IClusterCreator, ClusterCreator>()
         // .AddSingleton<IPlanetarySystemCreator, PlanetarySystemCreator>()
         ;

      return services;
   }
}
