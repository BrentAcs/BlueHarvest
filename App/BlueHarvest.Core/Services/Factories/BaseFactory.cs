using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Models.Geometry;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services.Factories;

public abstract class BaseFactory
{
   protected BaseFactory(IRng rng)
   {
      Rng = rng;
   }

   protected IRng Rng { get; }

   protected IEnumerable<Point3D> GeneratePointsInEllipsoid(
      int count,
      Ellipsoid ellipsoid,
      MinMax<double> distanceBetween,
      IEnumerable<Point3D>? existing = null)
   {
      existing = existing ?? new List<Point3D>();
      var points = new List<Point3D>();

      for (int i = 0; i < count; i++)
      {
         bool toClose = true;
         while (toClose)
         {
            var pt = Rng.CreateRandomInside(ellipsoid);
            toClose = points.Any(p => p.DistanceTo(pt) < distanceBetween.Min) || existing.Any(p => p.DistanceTo(pt) < distanceBetween.Min);
            if (!toClose)
            {
               points.Add(pt);
            }
         }
      }

      return points;
   }
}
