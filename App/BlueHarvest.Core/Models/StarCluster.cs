using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.StarClusters)]
public class StarCluster : Document
{
   public string? Name { get; set; }
   public string? Description { get; set; }
   public string? Owner { get; set; }
   public DateTime CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}
