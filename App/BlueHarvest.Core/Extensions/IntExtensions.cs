namespace BlueHarvest.Core.Extensions
{
   public static class IntExtensions
   {
      public static bool IsEven(this int value) =>
         value % 2 == 0;
      
      public static bool IsOdd(this int value) =>
         value % 2 != 0;
      
      public static bool IsPositive(this int value) =>
         value > 0;
      
      public static bool IsNegative(this int value) =>
         value < 0;
   }
}
