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
      
      public static readonly Style TableHeaderStyle = new Style(Color.Yellow, null, Decoration.Italic);
      public static readonly Style TableLabelStyle = new Style(Color.LightSkyBlue1, null, Decoration.Italic);
      public static readonly Style TableItemStyle = new Style(Color.LightGreen, null, Decoration.Italic);
   }

   public TableBorder MainTableBorder { get; set; } = Defaults.MainTableBorder;
   public Color MainTableBorderColor { get; set; } = Defaults.MainTableBorderColor;
   public int MainTableWidth { get; set; } = Defaults.MainTableWidth;

   public Style TableHeaderStyle { get; set; } = Defaults.TableHeaderStyle;
   public Style TableLabelStyle { get; set; } = Defaults.TableLabelStyle;
   public Style TableItemStyle { get; set; } = Defaults.TableItemStyle;
}
