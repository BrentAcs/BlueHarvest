namespace BlueHarvest.Core.Models.Cosmic;

/// <summary>
/// The central object of a planetary system
/// </summary>
public class Star
{
   public enum StarType
   {
      ClassB = 1,
      ClassA,
      ClassF,
      ClassG,
      ClassK,
   }

   public StarType Type { get; set; }
   public double Mass { get; set; }
}
