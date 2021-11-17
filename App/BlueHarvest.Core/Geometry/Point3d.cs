namespace BlueHarvest.Core.Geometry;

public record Point3D(double X = 0.0, double Y = 0.0, double Z = 0.0)
{
   public double X { get; set; } = X;
   public double Y { get; set; } = Y;
   public double Z { get; set; } = Z;

   public override string ToString() => $"[ {X:0.0000}, {Y:0.0000}, {Z:0.0000}]";

   public double DistanceTo(Point3D that)
   {
      var result = 0.0;

      var a = X - that.X;
      var b = Y - that.Y;
      var c = Z - that.Z;

      result = Math.Sqrt((a * a) + (b * b) + (c * c));
      return result;
   }

   // public static Point3D CreateRandomInside(Cuboid cuboid, IRng rng = null)
   // {
   //   rng = rng ?? Rng.Instance;
   //
   //   var result = new Point3D(
   //     rng.Next(0.0, cuboid.Width),
   //     rng.Next(0.0, cuboid.Length),
   //     rng.Next(0.0, cuboid.Height));
   //
   //   return result;
   // }
}
