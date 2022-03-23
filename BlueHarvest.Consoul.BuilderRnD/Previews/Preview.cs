using BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

namespace BlueHarvest.Consoul.BuilderRnD.Previews;

public abstract class Preview
{
   public static readonly StarClusterPreview StarCluster = new();
   public static readonly PlanetarySystemPreview PlanetarySystem = new();
   
   protected virtual string? Header => null;
   protected virtual string? Footer => null;
   protected virtual string? Prompt => "Press any key to continue.";
   protected abstract void ShowPreview();

   public void Show()
   {
      if (Header is not null)
         AnsiConsole.WriteLine($"{Header}");

      ShowPreview();

      if (Footer is not null)
         AnsiConsole.WriteLine($"{Footer}");

      AnsiConsole.WriteLine($"{Prompt}");
      Console.ReadKey(true);
   }
}
