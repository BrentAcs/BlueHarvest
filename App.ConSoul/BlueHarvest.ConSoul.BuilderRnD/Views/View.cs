namespace BlueHarvest.ConSoul.BuilderRnD.Views;

public abstract class View
{
   protected virtual string? Header => null;
   protected virtual string? Footer => null;
   
   protected abstract void ShowView();

   public void Show()
   {
      if (Header is not null)
         AnsiConsole.WriteLine($"{Header}");

      ShowView();

      if (Footer is not null)
         AnsiConsole.WriteLine($"{Footer}");

      //AnsiConsole.WriteLine($"{Prompt}");
      Console.ReadKey(true);
   }
}

public class StarClusterView : View
{
   protected override void ShowView() =>
      Console.WriteLine("Star Cluster View");
}

public class PlanetarySystemView : View
{
   protected override void ShowView() =>
      Console.WriteLine("Planetary System View");
}

public class SatelliteSystemView : View
{
   protected override void ShowView() =>
      Console.WriteLine("Satellite System View");
}
