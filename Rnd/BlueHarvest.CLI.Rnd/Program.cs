using System.Reflection;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using BlueHarvest.Core.Utilities;
using static System.Console;

namespace BlueHarvest.CLI.Rnd;

class Program
{
   static async Task Main(string[] args)
   {
      var rng = new SimpleRng();
      var col = new ChanceTable<StarType>();
      col.Add(StarType.ClassB, 0.625);
      col.Add(StarType.ClassA, 3.125);
      col.Add(StarType.ClassF, 15.0);
      col.Add(StarType.ClassG, 38.5);
      col.Add(StarType.ClassK, 41.5);

      var results = new Dictionary<StarType, int>();
      for (int i = 0; i < 100000; i++)
      {
         var star = col.GetItem(rng.Next(0.0, 100.00));
         if (!results.ContainsKey(star))
            results[ star ] = 1;
         else
            results[ star ]++;
      }

      foreach (var pair in results)
      {
         WriteLine($"{pair.Key}: {pair.Value}");
      }      
      
      WriteLine("Done.");
      ReadKey();
   }
}

