namespace BlueHarvest.Core.Utilities;

public interface IRng
{
   int Next();
   int Next(int maxValue);
   int Next(int minValue, int maxValue);
   double Next(double minimum, double maximum);
}
