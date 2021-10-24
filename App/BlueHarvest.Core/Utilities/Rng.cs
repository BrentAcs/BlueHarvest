using System;

namespace BlueHarvest.Core.Utilities
{
   public interface IRng
   {
      int Next();
      int Next(int maxValue);
      int Next(int minValue, int maxValue);
      double Next(double minimum, double maximum);
   }

   public sealed class SimpleRng : IRng
   {
      private readonly Random _random = new Random();

      public int Next() =>
         _random.Next();

      public int Next(int maxValue) =>
         _random.Next(maxValue);

      public int Next(int minValue, int maxValue) =>
         _random.Next(minValue, maxValue);

      public double Next(double minimum, double maximum) =>
         _random.NextDouble() * (maximum - minimum) + minimum;
   }
}
