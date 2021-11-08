namespace BlueHarvest.Core.Geometry;

public class Point3D
{
   protected bool Equals(Point3D other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

   public override bool Equals(object obj)
   {
      if (ReferenceEquals(null, obj))
      {
         return false;
      }

      if (ReferenceEquals(this, obj))
      {
         return true;
      }

      if (obj.GetType() != this.GetType())
      {
         return false;
      }

      return Equals((Point3D)obj);
   }

   public override int GetHashCode() => HashCode.Combine(X, Y, Z);

   public static bool operator ==(Point3D left, Point3D right) => Equals(left, right);

   public static bool operator !=(Point3D left, Point3D right) => !Equals(left, right);

   public Point3D(double x = 0.0, double y = 0.0, double z = 0.0)
   {
      X = x;
      Y = y;
      Z = z;
   }

   public double X { get; set; }
   public double Y { get; set; }
   public double Z { get; set; }

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
