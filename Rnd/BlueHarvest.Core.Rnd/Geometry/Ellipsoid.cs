namespace BlueHarvest.Core.Rnd.Geometry;

public interface IHaveVolume
{
   double Volume { get; }
}

public record Ellipsoid(double XRadius = 0.0, double YRadius = 0.0, double ZRadius = 0.0) : IHaveVolume
{
   public static readonly Ellipsoid Empty = new();
   
   public double XRadius { get; set; } = XRadius;
   public double YRadius { get; set; } = YRadius;
   public double ZRadius { get; set; } = ZRadius;

   [System.Text.Json.Serialization.JsonIgnore]
   public double XDiameter => XRadius * 2;

   [System.Text.Json.Serialization.JsonIgnore]
   public double YDiameter => YRadius * 2;

   [System.Text.Json.Serialization.JsonIgnore]
   public double ZDiameter => ZRadius * 2;

   [System.Text.Json.Serialization.JsonIgnore]
   public double Volume => (4.0 / 3.0) * Math.PI * XRadius * YRadius * ZRadius;
   
   public bool ContainsPoint(Point3D point, Point3D? centeredAt = null)
   {
      centeredAt ??= new Point3D();

      // Ref:  https://www.geeksforgeeks.org/check-if-a-point-is-inside-outside-or-on-the-ellipse/
      double position = (Math.Pow((point.X - centeredAt.X), 2) / Math.Pow(XRadius, 2)) +
                        (Math.Pow((point.Y - centeredAt.Y), 2) / Math.Pow(YRadius, 2)) +
                        (Math.Pow((point.Z - centeredAt.Z), 2) / Math.Pow(ZRadius, 2));

      return !(position > 1.0);
   }

   public override string ToString() => $"[ A:{XRadius:0.0000}, B:{YRadius:0.0000}, C:{ZRadius:0.0000}]";
}

public record Sphere : Ellipsoid
{
   public Sphere(double radius)
      : base(radius, radius, radius)
   {
   }
}
