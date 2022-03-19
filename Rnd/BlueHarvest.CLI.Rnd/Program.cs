using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Extensions;
using static System.Console;

WriteLine("Blue Harvest CLI Rnd");

var jsonSettings = new JsonSerializerSettings
{
   Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore, TypeNameHandling = TypeNameHandling.All
};
jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());


var p1 = Fake.CreatePlanet();
var s1 = Fake.CreateNaturalSatellite();


//var json =p1.AsJson(settings: jsonSettings);
var json = s1.AsJson(settings: jsonSettings);

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
