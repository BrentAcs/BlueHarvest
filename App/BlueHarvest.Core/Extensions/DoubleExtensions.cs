using System;

namespace BlueHarvest.Core.Extensions
{
   public static class DoubleExtensions
   {
      public static bool EqualsToPrecision(this double left, double right, double precision = 0.0001)
      {
#if DEBUG
         var test = Math.Abs(left - right);
#endif

         return Math.Abs(left - right) < precision;
      }
   }
}
