namespace BlueHarvest.Core.Rnd.Grids;

public interface IHexGrid
{
   float HexWidth { get; set; }
   float HexHeight { get; set; }
}

public abstract class HexGrid : IHexGrid
{
   public abstract float HexWidth { get; set; }
   public abstract float HexHeight { get; set; }
}
