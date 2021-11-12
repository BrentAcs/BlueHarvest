using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Rnd.Models;

public abstract class Document
{
   private static long _nextId = 0;

   protected Document()
   {
      Id = ++_nextId;
   }

   public long Id { get; set; }
}

public class StarCluster : Document
{
   public string? Owner { get; set; }
   public string? Description { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}

public abstract class InterstellarObject : Document
{
   public long ClusterId { get; set; }

   public Point3D? Location { get; set; }
   public string? Name { get; set; }
}

public abstract class StellarObject //: Document
{
   public string? Name { get; set; }
}
