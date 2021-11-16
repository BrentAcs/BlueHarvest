namespace BlueHarvest.Core.Geometry;

public class Ellipsoid : IHaveVolume
{
   protected bool Equals(Ellipsoid other) =>
      XRadius.Equals(other.XRadius) && YRadius.Equals(other.YRadius) && ZRadius.Equals(other.ZRadius);

   public override bool Equals(object? obj)
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

      return Equals((Ellipsoid)obj);
   }

   public override int GetHashCode() => HashCode.Combine(XRadius, YRadius, ZRadius);

   public static bool operator ==(Ellipsoid left, Ellipsoid right) => Equals(left, right);

   public static bool operator !=(Ellipsoid left, Ellipsoid right) => !Equals(left, right);

   public Ellipsoid(double xRadius = 0.0, double yRadius = 0.0, double zRadius = 0.0)
   {
      XRadius = xRadius;
      YRadius = yRadius;
      ZRadius = zRadius;
   }

   public double XRadius { get; set; }
   public double YRadius { get; set; }
   public double ZRadius { get; set; }

   [JsonIgnore]
   public double XDiameter => XRadius * 2;

   [JsonIgnore]
   public double YDiameter => YRadius * 2;

   [JsonIgnore]
   public double ZDiameter => ZRadius * 2;

   [JsonIgnore]
   public double Volume => (4.0 / 3.0) * Math.PI * XRadius * YRadius * ZRadius;

   public bool ContainsPoint(Point3D point, Point3D? centeredAt = null)
   {
      centeredAt ??= new Point3D();

      // Ref:  https://www.geeksforgeeks.org/check-if-a-point-is-inside-outside-or-on-the-ellipse/
      double position = (Math.Pow((point.X - centeredAt.X), 2) / Math.Pow(XRadius, 2)) +
                        (Math.Pow((point.Y - centeredAt.Y), 2) / Math.Pow(YRadius, 2)) +
                        (Math.Pow((point.Z - centeredAt.Z), 2) / Math.Pow(ZRadius, 2));

      if (position > 1.0)
         return false;

      return true;
   }

   public override string ToString() => $"[ A:{XRadius:0.0000}, B:{YRadius:0.0000}, C:{ZRadius:0.0000}]";
}
