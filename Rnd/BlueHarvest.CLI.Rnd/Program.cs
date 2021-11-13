using BlueHarvest.Core.Builders;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Rnd.Models;
using BlueHarvest.Core.Utilities;
using StarClusterBuilder = BlueHarvest.Core.Rnd.Builders.StarClusterBuilder;

namespace BlueHarvest.CLI.Rnd;

class Program
{
   static async Task Main(string[] args)
   {
      // var rng = new SimpleRng();
      //
      // var cluster = new StarClusterBuilder(rng, new EntityDesignator(rng))
      //    .Build(StarClusterBuilderOptions.Test);
      //
      //
      // //Console.WriteLine($"{cluster.ToJsonIndented()}");
      //
      // File.WriteAllText(@"c:\t\starcluster.json", cluster.ToJsonIndented());
      Console.WriteLine("Done.");
   } 
}


