using BlueHarvest.Core.Rnd.Geometry;

namespace BlueHarvest.Core.Rnd;

public interface IRootModel
{
}

public interface IMongoDocument
{
   // [BsonId]
   // [BsonRepresentation(BsonType.ObjectId)]
   // [JsonConverter(typeof(ObjectIdConverter))]
   // ObjectId Id { get; set; }
}

// ------------------------------------------------------------------------------------------------
// --- StarCluster

/// <summary>
/// The overall 'world' object. All things exist inside a star cluster
/// Size is measured in light-years
/// </summary>
//[BsonCollection(CollectionNames.StarClusters)]
public class StarCluster : IMongoDocument, IRootModel
{
   //public ObjectId Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
   public List<InterstellarObject> InterstellarObjects { get; set; } = new();
}

// ------------------------------------------------------------------------------------------------
// --- Interstellar Objects

/// <summary>
/// Base of objects found in interstellar space.
/// Distances between these objects is measured in light-years
/// </summary>
public abstract class InterstellarObject : IRootModel
{
   // [JsonConverter(typeof(ObjectIdConverter))]
   // public ObjectId ClusterId { get; set; }
   public Point3D? Location { get; set; }
   public string? Name { get; set; }
}

public class DeepSpaceObject : InterstellarObject
{
}

//[BsonCollection(CollectionNames.PlanetarySystems)]
public class PlanetarySystem : InterstellarObject, IMongoDocument
{
   // public ObjectId Id { get; set; }
   public Star? Star { get; set; } = new(); 
   public Sphere? Size { get; set; } = new(20);
   public List<StellarObject> StellarObjects { get; set; } = new();
}

// ------------------------------------------------------------------------------------------------
// --- Stellar Objects

/// <summary>
/// Base of objects found in stellar space.
/// Distances between these objects is measured in astronautical units
/// </summary>
public abstract class StellarObject : IRootModel
{
   public string? Name { get; set; }
   public Point3D? Location { get; set; }
   //public double? Distance { get; set; }
}

public class SatelliteSystem : StellarObject
{
   public Planet? Planet { get; set; } = new();
   public List<Satellite>? Satellites { get; set; }
}

public class AsteroidField : StellarObject
{
  
}

// ------------------------------------------------------------------------------------------------
// --- Satellite Objects

/// <summary>
/// Base of objects found in satellite system
/// Distances between these objects is measured in kilometers
/// </summary>
public abstract class Satellite : IRootModel
{
   public string? Name { get; set; }
   public double? Distance { get; set; }
}

public class NaturalSatellite : Satellite
{
}

public class ArtificialSatellite : Satellite
{
}

// ------------------------------------------------------------------------------------------------
// --- Objects

/// <summary>
/// The central object of a planetary system
/// </summary>
public class Star
{
   public StarType StarType { get; set; }
   public double Mass { get; set; }
}

/// <summary>
/// The central object of a satellite system
/// </summary>
public class Planet
{
   public PlanetType PlanetTypeType { get; set; }
   public double Mass { get; set; }
}
