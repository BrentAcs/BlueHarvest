using System.Reflection;
using BlueHarvest.Core.Misc;
using BlueHarvest.Core.Models;
using BlueHarvest.Core.Storage;
using static System.Console;

namespace BlueHarvest.CLI.Rnd;

class Program
{
   static async Task Main(string[] args)
   {

      WriteLine("Done.");
      ReadKey();
   }
}

public class Rnd
{
   public enum StarType
   {
      ClassB = 1,
      ClassA,
      ClassF,
      ClassG,
      ClassK,
   }

   // ref(s): https://nineplanets.org/star/
   public class StarDescriptor
   {
      public StarType StarType { get; set; }
      public string? TypeName { get; set; }
      public MinMax<double> Mass { get; set; }
   }
   
   
   
}
