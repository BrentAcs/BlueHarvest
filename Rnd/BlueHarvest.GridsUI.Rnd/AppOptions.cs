namespace BlueHarvest.GridsUI.Rnd;

public class AppOptions
{
   public FormOptions MainForm { get; set; } = new FormOptions();
}

public class FormOptions
{
   public Point Location { get; set; } = new Point(0, 0);
   public Size Size { get; set; } = new Size(1024, 768);

   public void Load(Form form)
   {
      form.Location = Location;
      form.Size = Size;
   }

   public void Save(Form form)
   {
      Location = form.Location;
      Size = form.Size;
   }
}
