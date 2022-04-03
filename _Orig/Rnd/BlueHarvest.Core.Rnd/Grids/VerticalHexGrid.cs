// /-\
// \-/ = Vertical

namespace BlueHarvest.Core.Rnd.Grids;

public class VerticalHexGrid : HexGrid
{
   public override float HexWidth { get; set; } = 100;
   public override float HexHeight
   {
      get => CalcHexHeight(HexWidth);
      set => throw new InvalidOperationException();
   }

   private static float CalcHexHeight(float height)
      => (float)(4 * (height / 2 / Math.Sqrt(3)));
}
