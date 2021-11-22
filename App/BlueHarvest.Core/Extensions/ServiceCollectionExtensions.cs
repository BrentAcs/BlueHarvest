﻿using BlueHarvest.Core.Models;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Services;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Storage.Repos;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddBlueHarvestCommon(this IServiceCollection services,
      IEnumerable<Assembly>? assemblies)
   {
      services
         // https://medium.com/dotnet-hub/use-mediatr-in-asp-net-or-asp-net-core-cqrs-and-mediator-in-dotnet-how-to-use-mediatr-cqrs-aspnetcore-5076e2f2880c
         .AddMediatR(assemblies.ToArray())
         .AddAutoMapper(assemblies)
         .AddSingleton<IEntityDesignator, EntityDesignator>()
         .AddSingleton<IRng, SimpleRng>()
         .AddScoped<IPlanetDescriptorService, PlanetDescriptorService>()
         ;

      //  ref: https://stackoverflow.com/questions/57015856/invalidoperationexception-cant-compile-a-newexpression-with-a-constructor-decl
      Storage.Misc.RegisterKnownTypes<StarCluster>();

      return services;
   }

   public static IServiceCollection AddBlueHarvestMongo(this IServiceCollection services,
      IConfiguration configuration)
   {
      services
         .Configure<MongoDbSettings>(configuration.GetSection("MongoDb"))
         .AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value)
         .AddSingleton<IMongoContext, MongoContext>()
         
         .AddScoped<IStarClusterRepo, StarStarClusterRepo>()
         .AddScoped<IPlanetarySystemRepo, PlanetarySystemRepo>()
         .AddScoped<IPlanetDescriptorRepo, PlanetDescriptorRepo>()
         .AddScoped<IStarDescriptorRepo, StarDescriptorRepo>()

         .AddScoped<IMongoRepository, StarStarClusterRepo>()
         .AddScoped<IMongoRepository, PlanetarySystemRepo>()
         .AddScoped<IMongoRepository, PlanetDescriptorRepo>()
         .AddScoped<IMongoRepository, StarDescriptorRepo>()
         ;

      return services;
   }
}
