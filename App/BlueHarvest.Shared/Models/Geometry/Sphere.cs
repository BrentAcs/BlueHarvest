namespace BlueHarvest.Shared.Models.Geometry;

public record Sphere : Ellipsoid
{
   public Sphere(double radius)
      : base(radius, radius, radius)
   {
   }
}
