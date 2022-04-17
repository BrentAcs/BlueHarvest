namespace BlueHarvest.Core.Utilities;

public sealed class SimpleRng : IRng
{
   public static readonly IRng Instance = new SimpleRng();

   private readonly Random _random = new();

   public int Next() => _random.Next();
   public int Next(int maxValue) => _random.Next(maxValue);
   public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);
   public double Next(double minimum, double maximum) => _random.NextDouble() * (maximum - minimum) + minimum;
   public int NextD4() => _random.Next(1, 5);
   public int NextD6() => _random.Next(1, 7);
   public int NextD8() => _random.Next(1, 9);
   public int NextD10() => _random.Next(1, 11);
   public int NextD100() => _random.Next(1, 101);
}
