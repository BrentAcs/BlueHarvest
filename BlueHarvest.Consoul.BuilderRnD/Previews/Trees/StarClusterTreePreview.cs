using BlueHarvest.Consoul.BuilderRnD.Previews.Trees;
using BlueHarvest.Consoul.Common;
using BlueHarvest.Core.Rnd;
using BlueHarvest.Core.Rnd.Utilities;

namespace BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

public class StarClusterTreePreview : TreePreview
{
   protected override string Header => "Star Cluster Tree Preview";

   protected override void ShowPreview()
   {
      // FakeFactory.Shallow = true;
      // var col = new List<StarCluster>();
      // for (int i = 0; i < 50; i++)
      // {
      //    col.Add(FakeFactory.CreateStarCluster());
      // }
      //
      // var table = col.Build();
      // AnsiConsole.Write(table);

      //EntityMonikerGeneratorService.Default.Generate();

      var moniker = EntityMonikerGeneratorService.Default;

      var root = new Tree(moniker.Generate());
      root.AddNode(moniker.Generate());
      var node = root.AddNode(moniker.Generate()).Collapse();
      node.AddNode("child");
      //.Collapse();
      root.AddNode(moniker.Generate());

      AnsiConsole.Write(root);
      
      AnsiConsole.WriteLine("presto!");
      Console.ReadKey(true);
      node.Expand();
   }
}
