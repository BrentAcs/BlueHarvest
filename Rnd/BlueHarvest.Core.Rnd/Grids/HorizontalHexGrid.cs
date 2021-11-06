// /\
// || = Horizontal
// \/

namespace BlueHarvest.Core.Rnd.Grids;

public class HorizontalHexGrid : HexGrid
{
   public override float HexWidth
   {
      get => CalcHexWidth(HexHeight);
      set => throw new InvalidOperationException();
   }
   public override float HexHeight { get; set; } = 100;


   private static float CalcHexWidth(float height)
      => (float)(4 * (height / 2 / Math.Sqrt(3)));
}
