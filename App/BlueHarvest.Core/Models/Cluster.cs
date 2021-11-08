using BlueHarvest.Core.Storage;

namespace BlueHarvest.Core.Models;

[BsonCollection(CollectionNames.Clusters)]
public class Cluster : Document
{
   public string Owner { get; set; }
   public string Description { get; set; }
   public DateTime CreatedOn { get; set; }
   //public Ellipsoid Size { get; set; }
}
