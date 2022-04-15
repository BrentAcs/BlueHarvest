namespace BlueHarvest.Core.Utilities;

public sealed class SimpleRng : IRng
{
   public static readonly IRng Instance = new SimpleRng(); 
   
   private readonly Random _random = new();

   public int Next() =>
      _random.Next();

   public int Next(int maxValue) =>
      _random.Next(maxValue);

   public int Next(int minValue, int maxValue) =>
      _random.Next(minValue, maxValue);

   public double Next(double minimum, double maximum) =>
      _random.NextDouble() * (maximum - minimum) + minimum;
}
