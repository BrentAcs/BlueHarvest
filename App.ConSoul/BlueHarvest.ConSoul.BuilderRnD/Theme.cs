namespace BlueHarvest.ConSoul.BuilderRnD;

public class Theme
{
   public static readonly Theme Default = new();
   public static Theme Active = Default;
   
   public class Defaults
   {
      public static readonly TableBorder MainTableBorder = TableBorder.DoubleEdge;
      public static readonly Color MainTableBorderColor = Color.DarkSlateGray1;
      public static readonly int MainTableWidth = 120;
   }

   public TableBorder MainTableBorder { get; set; } = Defaults.MainTableBorder;
   public Color MainTableBorderColor { get; set; } = Defaults.MainTableBorderColor;
   public int MainTableWidth { get; set; } = Defaults.MainTableWidth;
}
