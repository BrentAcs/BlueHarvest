using BlueHarvest.Core.Infrastructure.Storage;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using Microsoft.Extensions.Configuration;

namespace BlueHarvest.Core.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddBlueHarvestCommon(this IServiceCollection services,
      IEnumerable<Assembly> assemblies)
   {
      services
         // https://medium.com/dotnet-hub/use-mediatr-in-asp-net-or-asp-net-core-cqrs-and-mediator-in-dotnet-how-to-use-mediatr-cqrs-aspnetcore-5076e2f2880c
         //.AddMediatR(assemblies.ToArray())
         //.AddAutoMapper(assemblies)
         .AddSingleton<IMonikerGeneratorService, MonikerGeneratorService>()
         .AddSingleton<IMonikerGenerator, MonikerGenerator>()
         .AddSingleton<IRng, SimpleRng>()
         .AddScoped<IPlanetFactory, PlanetFactory>()
         .AddScoped<IStarFactory, StarFactory>()
         .AddScoped<IPlanetaryDistanceFactory, PlanetaryDistanceFactory>()
         .AddScoped<ISatelliteSystemFactory, SatelliteSystemFactory>()
         .AddScoped<IPlanetarySystemFactory, PlanetarySystemFactory>()
         .AddScoped<IStarClusterFactory, StarClusterFactory>()
         .AddScoped<IAppStateService, AppStateService>()
         ;

      //  ref: https://stackoverflow.com/questions/57015856/invalidoperationexception-cant-compile-a-newexpression-with-a-constructor-decl
      Misc.RegisterKnownTypes<StarCluster>();
      Misc.RegisterKnownTypes<InterstellarObject>();
      Misc.RegisterKnownTypes<StellarObject>();
      Misc.RegisterKnownTypes<Satellite>();

      return services;
   }

   public static IServiceCollection AddBlueHarvestMongo(this IServiceCollection services, IConfiguration configuration)
   {
      services
         .Configure<MongoDbSettings>(configuration.GetSection("MongoDb"))
         .AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
         .AddSingleton<IMongoContext, MongoContext>()
         
         .AddScoped<IStarClusterRepo, StarStarClusterRepo>()
         .AddScoped<IPlanetarySystemRepo, PlanetarySystemRepo>()
         .AddScoped<IUserAppStateRepo, UserAppStateRepo>()

         .AddScoped<IMongoRepository, StarStarClusterRepo>()
         .AddScoped<IMongoRepository, PlanetarySystemRepo>()
         .AddScoped<IMongoRepository, UserAppStateRepo>()
         ;

      return services;
   }
}
