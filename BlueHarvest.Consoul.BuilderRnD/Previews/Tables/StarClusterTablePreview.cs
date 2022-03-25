using BlueHarvest.Consoul.BuilderRnD.Menus;
using BlueHarvest.Consoul.Common;
using BlueHarvest.Core.Rnd;

namespace BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

public class StarClusterTablePreview : TablePreview
{
   protected override string Header => "Star Cluster Table Preview";

   protected override void ShowPreview()
   {
      // var items = new List<IndexedPromptString>();
      // items.Add(IndexedPromptString.None);

      FakeFactory.Shallow = true;
      var col = new List<StarCluster>();
      for (int i = 0; i < 20; i++)
      {
         var cluster = FakeFactory.CreateStarCluster();
         col.Add(cluster);
         // items.Add(IndexedPromptString.Create(i + 1, cluster.Name));
      }

      var table = col.Build();
      AnsiConsole.Write(table);
      //
      // var result = AnsiConSoul.Prompt(items, "pick one, bitch", 4);
      // AnsiConsole.WriteLine($"{result.Index} - {result.Item} - {result.Text}");
   }
}
