using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Storage.Services;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddBlueHarvestCommon(this IServiceCollection services,
      IConfiguration configuration)
   {
      services
         .Configure<MongoDbSettings>(configuration.GetSection("MongoDb"))
         .AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
         .AddSingleton<IMongoContext, MongoContext>()
         .AddScoped<IStarClusterRepo, StarStarClusterRepo>()
         .AddScoped<IPlanetarySystemRepo, PlanetarySystemRepo>()
         .AddSingleton<IEntityDesignator, EntityDesignator>()
         .AddSingleton<IRng, SimpleRng>()
         .AddScoped<ICollectionsService, CollectionsService>()
         .AddSingleton<IStarTypeService, StarTypeService>()
         .AddSingleton<IStarClusterBuilder, StarClusterBuilder>()
         .AddSingleton<IPlanetarySystemBuilder, PlanetarySystemBuilder>()
         ;

      //  ref: https://stackoverflow.com/questions/57015856/invalidoperationexception-cant-compile-a-newexpression-with-a-constructor-decl
      Storage.Misc.RegisterKnownTypes<StellarObject>();
      
      return services;
   }
}
