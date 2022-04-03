using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Geometry;
using BlueHarvest.Core.Rnd.Utilities;
using Faker;

namespace BlueHarvest.ConSoul.BuilderRnD;

public static class FakeFactory
{
   public class StarClusterOptions
   {
      public static readonly StarClusterOptions Empty = new() {PlanetarySystemCount = 0};

      public int? PlanetarySystemCount { get; set; }
      public MinMax<int> PlanetarySystemCountMinMax { get; set; } = new(50, 200);

      public PlanetarySystemOptions? PlanetarySystemOptions { get; set; } = new();
   }

   public class PlanetarySystemOptions
   {
      public static readonly PlanetarySystemOptions Empty = new()
      {
         SatelliteSystemCount = 0,
         AsteroidFieldCount = 0
      };

      public int? SatelliteSystemCount { get; set; }
      public MinMax<int> SatelliteSystemCountMinMax { get; set; } = new(5, 10);

      public int? AsteroidFieldCount { get; set; }
      public MinMax<int> AsteroidFieldCountMinMax { get; set; } = new(1, 4);
   }

   public class SatelliteSystemOptions
   {
      public static readonly SatelliteSystemOptions Empty = new()
      {
         MoonCount = 0,
         StationCount = 0
      };      
      
      public int? MoonCount { get; set; }
      public MinMax<int> MoonCountMinMax { get; set; } = new(1, 10);
      
      public int? StationCount { get; set; }
      public MinMax<int> StationCountMinMax { get; set; } = new(1, 10);
   }

   public static StarCluster CreateStarCluster(StarClusterOptions? options = null)
   {
      var cluster = new StarCluster
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Description = Lorem.Sentence(),
         Owner = Name.FullName(),
         CreatedOn = DateTime.Now,
         Size = CreateEllipsoid()
      };

      options ??= new StarClusterOptions();
      int planetSystemCount = options.PlanetarySystemCount ?? RandomNumber.Next(options.PlanetarySystemCountMinMax.Min, options.PlanetarySystemCountMinMax.Max);
      for (int i = 0; i < planetSystemCount; ++i)
      {
         var system = CreatePlanetarySystem(options.PlanetarySystemOptions);
         cluster.InterstellarObjects.Add(system);
      }

      return cluster;
   }

   public static PlanetarySystem CreatePlanetarySystem(PlanetarySystemOptions? options=null)
   {
      var planetarySystem = new PlanetarySystem()
      {
         Location = CreatePoint3D(),
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Star = CreateStar(),
         Size = CreateSphere()
      };

      options ??= new PlanetarySystemOptions();
      int satelliteSystemCount = options.SatelliteSystemCount ?? RandomNumber.Next(options.SatelliteSystemCountMinMax.Min, options.SatelliteSystemCountMinMax.Max);
      for (int i = 0; i < satelliteSystemCount; ++i)
      {
         var system = CreateSatelliteSystem();
         planetarySystem.StellarObjects.Add(system);
      }

      int asteroidFieldCount = options.AsteroidFieldCount ?? RandomNumber.Next(options.AsteroidFieldCountMinMax.Min, options.AsteroidFieldCountMinMax.Max);
      for (int i = 0; i < asteroidFieldCount; ++i)
      {
         var field = CreateAsteroidField();
         planetarySystem.StellarObjects.Add(field);
      }

      return planetarySystem;
   }

   public static SatelliteSystem CreateSatelliteSystem(SatelliteSystemOptions? options = null)
   {
      var satelliteSystem = new SatelliteSystem
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Location = CreatePoint3D(),
         Planet = CreatePlanet(),
      };

      options ??= new SatelliteSystemOptions();
      int moonCount = options.MoonCount ?? RandomNumber.Next(options.MoonCountMinMax.Min, options.MoonCountMinMax.Min);
      for (int i = 0; i < moonCount; ++i)
      {
         var moon = CreateNaturalSatellite();
         satelliteSystem.Satellites.Add(moon);
      }

      int stationCount = options.StationCount ?? RandomNumber.Next(options.StationCountMinMax.Min, options.StationCountMinMax.Max);
      for (int i = 0; i < stationCount; ++i)
      {
         var station = CreateArtificialSatellite();
         satelliteSystem.Satellites.Add(station);
      }

      return satelliteSystem;
   }

   public static AsteroidField CreateAsteroidField() =>
      new()
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Location = CreatePoint3D(),
         AsteroidCount = RandomNumber.Next(10, 100)
      };

   public static Point3D CreatePoint3D() =>
      new(RandomNumber.Next(10, 100), RandomNumber.Next(10, 100), RandomNumber.Next(10, 100));

   public static Ellipsoid CreateEllipsoid() =>
      new(RandomNumber.Next(10, 100), RandomNumber.Next(10, 100), RandomNumber.Next(10, 100));

   public static Sphere CreateSphere() =>
      new(RandomNumber.Next(10, 100));

   // ------------------------------------------------------------------------------------------------

   public static Planet CreatePlanet() =>
      new()
      {
         PlanetTypeType = Faker.Enum.Random<PlanetType>(),
         Mass = RandomNumber.Next(1000, 10000)
      };

   public static Star CreateStar() =>
      new()
      {
         StarType = Faker.Enum.Random<StarType>(),
         Mass = RandomNumber.Next(1000, 10000)
      };

   public static NaturalSatellite CreateNaturalSatellite() =>
      new()
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Distance = RandomNumber.Next(1000, 10000)
      };

   public static ArtificialSatellite CreateArtificialSatellite() =>
      new()
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Distance = RandomNumber.Next(100, 500)
      };
}
