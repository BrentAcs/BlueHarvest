using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Geometry;
using BlueHarvest.Core.Rnd.Utilities;
using Faker;

namespace BlueHarvest.Consoul.Common;

public static class FakeFactory
{
   public static bool Shallow { get; set; } = false;
   
   public static StarCluster CreateStarCluster(int? planetSystemCount = null)
   {
      var cluster = new StarCluster
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Description = Lorem.Sentence(),
         Owner = Name.FullName(),
         CreatedOn = DateTime.Now,
         Size = CreateEllipsoid()
      };

      if (Shallow)
         return cluster;
         
      planetSystemCount ??= RandomNumber.Next(50, 200);
      for (int i = 0; i < planetSystemCount; ++i)
      {
         var system = CreatePlanetarySystem();
         cluster.InterstellarObjects.Add(system);
      }

      return cluster;
   }

   public static PlanetarySystem CreatePlanetarySystem(int? satelliteSystemCount=null, int? asteroidFieldCount=null)
   {
      var planetarySystem = new PlanetarySystem()
      {
         Location = CreatePoint3D(),
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Star = CreateStar(), 
         Size = CreateSphere()
         // public List<StellarObject> StellarObjects { get; set; } = new();
      };

      satelliteSystemCount ??= RandomNumber.Next(5, 10);
      for (int i = 0; i < satelliteSystemCount; ++i)
      {
         var system = CreateSatelliteSystem();
         planetarySystem.StellarObjects.Add(system);
      }

      asteroidFieldCount ??= RandomNumber.Next(1, 4);
      for (int i = 0; i < asteroidFieldCount; ++i)
      {
         var field = CreateAsteroidField();
         planetarySystem.StellarObjects.Add(field);
      }
      
      return planetarySystem;
   }

   public static SatelliteSystem CreateSatelliteSystem(int? moonCount=null, int?stationCount=null)
   {
      var satelliteSystem = new SatelliteSystem
      {
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Location = CreatePoint3D(),
         Planet = CreatePlanet(),
      };

      moonCount ??= RandomNumber.Next(0, 3);
      for (int i = 0; i < moonCount; ++i)
      {
         var moon = CreateNaturalSatellite();
         satelliteSystem.Satellites.Add(moon);
      }

      stationCount ??= RandomNumber.Next(0, 3);
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
         AsteroidCount = RandomNumber.Next(10,100)
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
