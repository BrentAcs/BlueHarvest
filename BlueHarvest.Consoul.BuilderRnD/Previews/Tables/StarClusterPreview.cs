using BlueHarvest.Consoul.Common;
using BlueHarvest.Core.Rnd;

namespace BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

public class StarClusterPreview : TablePreview
{
   protected override string Header => "Star Cluster Preview";

   protected override void ShowPreview()
   {
      FakeFactory.Shallow = true;
      var col = new List<StarCluster>();
      for (int i = 0; i < 50; i++)
      {
         col.Add(FakeFactory.CreateStarCluster());
      }

      var table = col.Build();
      AnsiConsole.Write(table);
   }
}

public class PlanetarySystemPreview : TablePreview
{
   protected override string Header => "Planetary System Preview";
   
   protected override void ShowPreview()
   {
      FakeFactory.Shallow = true;
      var col = new List<PlanetarySystem>();
      for (int i = 0; i < 50; i++)
      {
         col.Add(FakeFactory.CreatePlanetarySystem());
      }

      var table = col.Build();
      AnsiConsole.Write(table);
   }
}
