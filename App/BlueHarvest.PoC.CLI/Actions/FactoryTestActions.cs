using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Services.Factories;
using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.PoC.CLI.Actions;

public class FactoryTestActions : MenuActions
{
   private readonly IRng _rng;
   private readonly IStarFactory _starFactory;
   private readonly IPlanetFactory _planetFactory;
   private readonly IPlanetaryDistanceFactory _planetaryDistanceFactory;
   private readonly ISatelliteSystemFactory _satelliteSystemFactory;
   private readonly IPlanetarySystemFactory _planetarySystemFactory;
   private readonly IStarClusterFactory _starClusterFactory;

   public FactoryTestActions(IRng rng,
      IStarFactory starFactory,
      IPlanetFactory planetFactory,
      IPlanetaryDistanceFactory planetaryDistanceFactory,
      ISatelliteSystemFactory satelliteSystemFactory,
      IPlanetarySystemFactory planetarySystemFactory,
      IStarClusterFactory starClusterFactory)
   {
      _rng = rng;
      _starFactory = starFactory;
      _planetFactory = planetFactory;
      _planetaryDistanceFactory = planetaryDistanceFactory;
      _satelliteSystemFactory = satelliteSystemFactory;
      _planetarySystemFactory = planetarySystemFactory;
      _starClusterFactory = starClusterFactory;
   }

   public bool SaveToFile { get; private set; }

   private void SaveObjectToFile<T>(T obj, string filename, JsonSerializerSettings settings = null)
   {
      if (!SaveToFile)
         return;

      string basePath = "../../../../../SampleData/";
      if (!Directory.Exists(basePath))
      {
         Directory.CreateDirectory(basePath);
      }

      settings ??= JsonSettings.FormattedTypedNamedEnums;
      string filePath = Path.ChangeExtension(Path.Combine(basePath, filename), ".json");

      obj.ToJsonFile(filePath, settings);
   }

   public void ToggleSaveToFile() =>
      SaveToFile = !SaveToFile;
   
   public void TestPlanetarySystemFactory()
   {
      ShowTitle("Test Planetary System Factory");
      var system = _planetarySystemFactory
         .Create(PlanetarySystemFactoryOptions.Test, ObjectId.Empty, new Point3D(42, 69, 0))
         .ConfigureAwait(false)
         .GetAwaiter()
         .GetResult();
      ShowResult(system);
      SaveObjectToFile(system, "planetary-system");
      ShowReturn();
   }

   public void TestSatelliteSystemFactory()
   {
      ShowTitle("Test Satellite System Factory");
      var system = _satelliteSystemFactory.Create(1.0);
      ShowResult(system);
      SaveObjectToFile(system, "satellite-system");
      ShowReturn();
   }

   public void TestPlanetDistanceFactory()
   {
      ShowTitle("Test Planet Distance Factory");
      var distances = _planetaryDistanceFactory.Create(20.0);
      var data = distances.Select(d => new
      {
         Distance = d,
         Zone = d.IdentifyPlanetaryZone()
      });
      ShowResult(data);
      SaveObjectToFile(data, "planet-distances");
      ShowReturn();
   }

   public void TestPlanetFactory()
   {
      ShowTitle("Test Planet Factory");
      var planet = _planetFactory.Create(1.0);
      ShowResult(planet);
      SaveObjectToFile(planet, "planet");
      ShowReturn();
   }

   public void TestStarFactory()
   {
      ShowTitle("Test Star Factory");
      var star = _starFactory.Create();
      ShowResult(star);
      SaveObjectToFile(star, "star");
      ShowReturn();
   }
}
