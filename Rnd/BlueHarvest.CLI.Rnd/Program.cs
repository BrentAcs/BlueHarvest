using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Extensions;
using BlueHarvest.Core.Rnd.Utilities;
using static System.Console;

WriteLine("Blue Harvest CLI Rnd");


// var p1 = FakeFactory.CreatePlanet();
// var s1 = FakeFactory.CreateNaturalSatellite();

EntityMonikerGeneratorService.Default.Reset();
var obj = FakeFactory.CreateStarCluster();
//var obj = FakeFactory.CreatePlanetarySystem();


var json = obj.AsJson(settings: JsonSettings.FormattedTypedNamedEnums);
obj.ToJsonFile(@"C:\Code\BlueHarvest\Logs\star-cluster.json", JsonSettings.FormattedNamedEnums);

WriteLine(json);

// var satelliteSystem = new SatelliteSystem {Name = "Earth satellite system", Planet = new Planet { }};
//
// var asteroidField = new AsteroidField {Name = "Example asteroid field"};
//
// var planetarySystem = new PlanetarySystem {Name = "Sol planetary system.", Star = new Star { }};
// planetarySystem.StellarObjects.Add(satelliteSystem);
// planetarySystem.StellarObjects.Add(asteroidField);
//
// var cluster = new StarCluster();
// cluster.InterstellarObjects.Add(planetarySystem);
//
//
// var json = cluster.AsJson(settings: jsonSettings);
// WriteLine(json);
//
// cluster.ToJsonFile(@"C:\Code\BlueHarvest\Logs\star-cluster.json", new JsonSerializerSettings {Formatting = Formatting.Indented});

//var sc2 = json.FromJson<StarCluster>(jsonSettings);

WriteLine("Done.");
ReadKey();
