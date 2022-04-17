using BlueHarvest.Core.Utilities;

namespace BlueHarvest.Core.Extensions;

public static class HaveChanceExtensions
{
   public static T GetByChance<T>(this IEnumerable<T> items, double roll) where T : IHaveChance
   {
      var sorted = items.OrderBy(i => i.Chance);
      double current = 0;

      foreach (var item in sorted)
      {
         current += item.Chance;
         if (roll < current)
            return item;
      }

      return sorted.Last();
   }
}
