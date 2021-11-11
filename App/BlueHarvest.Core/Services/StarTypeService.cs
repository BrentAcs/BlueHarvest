using BlueHarvest.Core.Models;
using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Services;

public interface IStarTypeService
{
   StarType GetRandomType();
}

public class StarTypeService : IStarTypeService
{
   private readonly IRng _rng;

   private class StarTemplate
   {
      public StarType Type { get; set; }
      public double Chance { get; set; }
   }

   private static readonly IList<StarTemplate> Templates = new List<StarTemplate>
   {
      new() {Type = StarType.ClassB, Chance = 0.625},
      new() {Type = StarType.ClassA, Chance = 3.125},
      new() {Type = StarType.ClassF, Chance = 15},
      new() {Type = StarType.ClassG, Chance = 38.5},
      new() {Type = StarType.ClassK, Chance = 41.5},
   };

   public StarTypeService(IRng rng)
   {
      _rng = rng;
   }

   public StarType GetRandomType()
   {
      double roll = _rng.Next(0, 100);
      foreach (var temp in Templates)
      {
         if (roll < temp.Chance)
            return temp.Type;
      }

      return StarType.ClassK;
   }
}
