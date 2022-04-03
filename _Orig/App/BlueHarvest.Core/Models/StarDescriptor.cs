using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.StarDescriptors)]
public class StarDescriptor : IMongoDocument
{
   public ObjectId Id { get; set; }
   public StarType StarType { get; set; }
   public string? Name { get; set; }
   public double Chance { get; set; }
   public MinMax<double>? MassRange { get; set; }
}
