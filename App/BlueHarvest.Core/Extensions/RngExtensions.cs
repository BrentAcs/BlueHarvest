using BlueHarvest.Core.Utilities;
using BlueHarvest.Shared.Models.Geometry;

namespace BlueHarvest.Core.Extensions;

public static class RngExtensions
{
   public static Point3D CreateRandomInside(this IRng rng, Ellipsoid ellipsoid)
   {
      Point3D result = new Point3D(
         rng.Next(-ellipsoid.XRadius, ellipsoid.XRadius),
         rng.Next(-ellipsoid.YRadius, ellipsoid.YRadius),
         rng.Next(-ellipsoid.ZRadius, ellipsoid.ZRadius));
      while (!ellipsoid.ContainsPoint(result))
      {
         result = new Point3D(
            rng.Next(-ellipsoid.XRadius, ellipsoid.XRadius),
            rng.Next(-ellipsoid.YRadius, ellipsoid.YRadius),
            rng.Next(-ellipsoid.ZRadius, ellipsoid.ZRadius));
      }

      return result;
   }

   public static double Next(this IRng rng, MinMax<double> range) =>
      rng.Next(range.Min, range.Max);
      
   public static int Next(this IRng rng, MinMax<int> range) =>
      rng.Next(range.Min, range.Max);
}
