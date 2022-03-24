using BlueHarvest.Consoul.BuilderRnD.Previews.Trees;
using BlueHarvest.Consoul.Common;
using BlueHarvest.Core.Rnd;

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
      
      AnsiConsole.WriteLine("boobs");
   }
}
