using BlueHarvest.Core.Rnd.Geometry;
using BlueHarvest.Core.Rnd.Utilities;
using Faker;

namespace BlueHarvest.Core.Rnd;

// ------------------------------------------------------------------------------------------------
// --- FakeFactory

public static class FakeFactory
{
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

      planetSystemCount ??= RandomNumber.Next(10, 20);
      for (int i = 0; i < planetSystemCount; ++i)
      {
         var system = CreatePlanetarySystem();
         cluster.InterstellarObjects.Add(system);
      }

      return cluster;
   }

   public static PlanetarySystem CreatePlanetarySystem()
   {
      var system = new PlanetarySystem()
      {
         Location = CreatePoint3D(),
         Name = EntityMonikerGeneratorService.Default.Generate(),
         Star = CreateStar(), 
         Size = CreateSphere()
         // public List<StellarObject> StellarObjects { get; set; } = new();
      };
      
      return system;
   }


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
}
