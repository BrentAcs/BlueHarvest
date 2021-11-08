namespace BlueHarvest.Core.Geometry;

public class Sphere : Ellipsoid
{
   public Sphere(double radius)
      : base(radius, radius, radius)
   {
   }
}
