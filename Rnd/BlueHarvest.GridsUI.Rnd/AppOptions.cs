using BlueHarvest.Core.Extensions;
using Newtonsoft.Json;

namespace BlueHarvest.GridsUI.Rnd;

public interface IAppOptions
{
   string Filename { get; }
   void Save(string? filename=default);
   
   FormOptions MainForm { get; }
}

public class AppOptions : IAppOptions
{
   [JsonIgnore]
   public string Filename { get; set; } = string.Empty;
   public FormOptions MainForm { get; set; } = new FormOptions();

   public void Save(string? filename = default)
   {
      filename ??= Filename;
      this.ToJsonFile(filename);
   }
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
