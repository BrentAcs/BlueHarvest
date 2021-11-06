namespace BlueHarvest.Core.Rnd.Grids;

public interface IHexGridFactory
{
   IHexGrid Create(HexGridType type);
}

public class HexGridFactory : IHexGridFactory
{
   public IHexGrid Create(HexGridType type)
   {
      switch (type)
      {
         case HexGridType.Horizontal:
            return new HorizontalHexGrid();
         case HexGridType.Vertical:
            return new VerticalHexGrid();
         default:
            throw new ArgumentOutOfRangeException(nameof(type), type, null);
      }
   }
}
