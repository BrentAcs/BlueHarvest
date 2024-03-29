﻿using BlueHarvest.Core.Exceptions;
using BlueHarvest.Core.Infrastructure.Storage.Repos;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Cosmic;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Services.Factories;

/// <summary>
/// Stock, for game play, star cluster builder
/// </summary>
public class StarClusterFactory : BaseFactory, IStarClusterFactory
{
   private readonly IStarClusterRepo _starClusterRepo;
   private readonly IPlanetarySystemFactory _planetarySystemFactory;

   public StarClusterFactory(IRng rng,
      IStarClusterRepo starClusterRepo, 
      IPlanetarySystemFactory planetarySystemFactory) : base(rng)
   {
      _starClusterRepo = starClusterRepo;
      _planetarySystemFactory = planetarySystemFactory;
   }

   public async Task<StarCluster> Create(StarClusterFactoryOptions options)
   {
      if (options is null)
         throw new ArgumentNullException(nameof(options));
      if (options.DesiredPlanetarySystems is null)
         throw ArgumentPropertyNullException.Create(nameof(options), nameof(options.DesiredPlanetarySystems));
      if (options.DesiredDeepSpaceObjects is null)
         throw ArgumentPropertyNullException.Create(nameof(options), nameof(options.DesiredDeepSpaceObjects));

      var cluster = new StarCluster
      {
         CreatedOn = DateTime.Now,
         Name = options.Name,
         Description = options.Description,
         Owner = options.Owner,
         Size = options.ClusterSize
      };

      await _starClusterRepo.InsertOneAsync(cluster).ConfigureAwait(false);

      int systemCount = options.DesiredPlanetarySystems.GetAmount(Rng);
      int deepSpaceCount = options.DesiredDeepSpaceObjects.GetAmount(Rng);
      if (systemCount + deepSpaceCount > options.MaximumPossibleSystems)
         throw BuilderException.CreateTooManyInterstellarObjects(options.MaximumPossibleSystems, systemCount + deepSpaceCount);

      var systemLocs = BuildPlanetarySystems(options, systemCount, cluster);
      _ = BuildDeepSpaceObjects(options, deepSpaceCount, systemLocs, cluster);
    
      return cluster;
   }

   private IEnumerable<Point3D> BuildPlanetarySystems(StarClusterFactoryOptions options, int systemCount, StarCluster cluster)
   {
      var systemLocs = GeneratePointsInEllipsoid(systemCount, options.ClusterSize, options.DistanceBetweenSystems);
      foreach (var location in systemLocs)
      {
         var system = _planetarySystemFactory.Create(options.PlanetarySystemOptions, cluster.Id, location);
         //cluster.InterstellarObjects.Add(system);
      }

      return systemLocs;
   }

   private IEnumerable<Point3D> BuildDeepSpaceObjects(StarClusterFactoryOptions options, int deepSpaceCount, IEnumerable<Point3D> systemLocs,
      StarCluster cluster)
   {
      var deepSpaceLocs = GeneratePointsInEllipsoid(deepSpaceCount, options.ClusterSize, options.DistanceBetweenSystems, systemLocs);
      foreach (var location in deepSpaceLocs)
      {
         var system = new DeepSpaceObject
         {
            Name = MonikerGenerator.Default.Generate(),
            Location = location
         };
         //cluster.InterstellarObjects.Add(system);
      }

      return deepSpaceLocs;
   }
};
