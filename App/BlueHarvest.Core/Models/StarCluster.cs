using BlueHarvest.Core.Geometry;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.Clusters)]
public class StarCluster : Document
{
   public string? Owner { get; set; }
   public string? Description { get; set; }
   public DateTime CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}
