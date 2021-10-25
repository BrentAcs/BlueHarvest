namespace BlueHarvest.GridsUI.Rnd;

public class AppOptions
{
   public FormOptions MainForm { get; set; } = new FormOptions();
}

public class FormOptions
{
   public Point Location { get; set; } = new Point(0, 0);
   public Size Size { get; set; } = new Size(1024, 768);
}
