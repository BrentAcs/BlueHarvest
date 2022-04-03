using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models.Cosmic;

[BsonCollection(CollectionNames.StarClusters)]
public class StarCluster : IMongoDocument, IRootModel
{
   public ObjectId Id { get; set; }
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}
