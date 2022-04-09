﻿using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models.Cosmic;

public abstract class InterstellarObject : IRootModel
{
   [JsonConverter(typeof(ObjectIdConverter))]
   public ObjectId ClusterId { get; set; }

   public Point3D? Location { get; set; }
   public string? Name { get; set; }
}