namespace BlueHarvest.Core.Extensions;

public static class DoubleExtensions
{
   public static bool EqualsToPrecision(this double left, double right, double precision = 0.0001)
   {
#if DEBUG
      var abs = Math.Abs(left - right);
#endif

      return Math.Abs(left - right) < precision;
   }

   public const double AuPerLightYear = 63241.1;
   public const double KmPerAu = 1.496e+8;
   
   public static double LightYearToAu(this double ly) =>
      ly * AuPerLightYear;

   public static double AuToLightYear(this double au) =>
      au / AuPerLightYear;

   public static double AuToKm(this double au) =>
      au * KmPerAu;
   
   public static double KmToAu(this double km) =>
      km / KmPerAu;

}

