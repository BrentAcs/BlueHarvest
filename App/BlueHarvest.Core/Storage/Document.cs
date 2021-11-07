﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace BlueHarvest.Core.Storage;

public interface IDocument
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   [JsonConverter(typeof(ObjectIdConverter))]
   ObjectId Id { get; set; }
}

public abstract class Document : IDocument
{
   public ObjectId Id { get; set; }
}
