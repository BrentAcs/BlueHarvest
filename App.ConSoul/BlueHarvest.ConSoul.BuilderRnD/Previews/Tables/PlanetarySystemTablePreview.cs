using BlueHarvest.ConSoul.Common;
using BlueHarvest.Core.Rnd;

namespace BlueHarvest.ConSoul.BuilderRnD.Previews.Tables;

public class PlanetarySystemTablePreview : TablePreview
{
   protected override string Header => "Planetary System Table Preview";
   
   protected override void ShowPreview()
   {
      Console.WriteLine("Address this");
      Console.ReadKey();
      //FakeFactory.Shallow = true;

      var col = new List<PlanetarySystem>();
      for (int i = 0; i < 50; i++)
      {
         col.Add(FakeFactory.CreatePlanetarySystem());
      }
      
      var table = col.Build();
      AnsiConsole.Write(table);
   }
}
