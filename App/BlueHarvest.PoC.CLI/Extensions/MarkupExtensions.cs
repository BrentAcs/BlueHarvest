using BlueHarvest.Core.Models.Geometry;

namespace BlueHarvest.PoC.CLI.Extensions;

public static class MarkupExtensions
{
   public static string ToMarkup(this Point3D point) => $"( {point.X:0.00}, {point.Y:0.00}, {point.Z:0.00} )";

   public static Markup ToMarkup(this Ellipsoid ellipsoid) =>
      new($"( {ellipsoid.XDiameter:0.00}, {ellipsoid.YDiameter:0.00}, {ellipsoid.ZDiameter:0.00} )");
}
