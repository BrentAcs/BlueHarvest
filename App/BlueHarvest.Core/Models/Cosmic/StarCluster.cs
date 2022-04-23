﻿using BlueHarvest.Core.Infrastructure.Storage;
using BlueHarvest.Core.Models.Geometry;

namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// The overall 'world' object. All things exist inside a star cluster
/// Size is measured in light-years
/// </summary>
[BsonCollection("StarClusters")]
public class StarCluster : IMongoDocument //, ISystemModel
{
   public ObjectId Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
   
   // public List<InterstellarObject> InterstellarObjects { get; set; } = new();
   //
   // [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   // public IEnumerable<PlanetarySystem> PlanetarySystems => InterstellarObjects.OfType<PlanetarySystem>();
   //
   // [System.Text.Json.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore]
   // public IEnumerable<DeepSpaceObject> DeepSpaceObjects => InterstellarObjects.OfType<DeepSpaceObject>();
}