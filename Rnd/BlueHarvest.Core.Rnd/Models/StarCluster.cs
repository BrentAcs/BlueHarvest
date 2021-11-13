using BlueHarvest.Core.Geometry;

namespace BlueHarvest.Core.Rnd.Models;

public abstract class RndDocument
{
   private static long _nextId = 0;

   protected RndDocument()
   {
      Id = ++_nextId;
   }

   public long Id { get; set; }
}

public class RndStarCluster : RndDocument
{
   public string? Owner { get; set; }
   public string? Description { get; set; }
   public DateTime? CreatedOn { get; set; }
   public Ellipsoid? Size { get; set; }
}

public abstract class RndInterstellarObject : RndDocument
{
   public long ClusterId { get; set; }

   public Point3D? Location { get; set; }
   public string? Name { get; set; }
}

public abstract class RndStellarObject : RndDocument
{
   public string? Name { get; set; }
}
