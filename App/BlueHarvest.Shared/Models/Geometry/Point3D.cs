namespace BlueHarvest.Shared.Models.Geometry;

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
}
