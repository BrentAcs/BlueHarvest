using BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

namespace BlueHarvest.Consoul.BuilderRnD.Previews;

public abstract class Preview
{
   public static readonly StarClusterTablePreview StarClusterTable = new();
   public static readonly PlanetarySystemTablePreview PlanetarySystemTable = new();

   public static readonly StarClusterTreePreview StarClusterTree = new();
   
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
