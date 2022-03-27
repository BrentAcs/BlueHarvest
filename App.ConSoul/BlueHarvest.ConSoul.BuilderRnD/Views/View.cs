using BlueHarvest.Core.Rnd;

namespace BlueHarvest.ConSoul.BuilderRnD.Views;

public abstract class View<T>
{
   protected virtual string? Header => null;
   protected virtual string? Footer => null;
   protected virtual ViewPrompt? ViewPrompt => null;

   protected abstract void ShowView();

   public ViewPromptItem? Show(T? source)
   {
      if (Header is not null)
         AnsiConsole.WriteLine($"{Header}");

      ShowView();

      if (Footer is not null)
         AnsiConsole.WriteLine($"{Footer}");

      ViewPromptItem? item = null;
      if (ViewPrompt != null)
      {
         item = ViewPrompt.Show();
         Console.WriteLine($"{item}");
         Console.ReadKey(true);
      }

      return item;
   }
}

public class ViewPromptItem
{
   public string? Display { get; set; }

   public ViewPromptItem(string? display)
   {
      Display = display;
   }

   public override string? ToString() => Display;
}

public abstract class ViewPrompt
{
   protected virtual int PageSize { get; set; } = 10;
   protected virtual string Title { get; set; } = "Please make a selection.";

   protected abstract void BuildChoices(SelectionPrompt<ViewPromptItem> prompt);

   public virtual ViewPromptItem Show()
   {
      var prompt = new SelectionPrompt<ViewPromptItem>()
         .PageSize(PageSize)
         .Title(Title)
         .AddChoices();

      BuildChoices(prompt);

      var item = AnsiConsole.Prompt(prompt);
      return item;
   }
}


public class StarClusterView : View<StarCluster>
{
   protected override ViewPrompt? ViewPrompt => BuildViewPrompt();

   private ViewPrompt? BuildViewPrompt()
   {
      return new StarClusterViewPrompt();
   }

   protected override void ShowView()
   {
      Console.WriteLine("Star Cluster View");
   }

   private class StarClusterViewPrompt : ViewPrompt
   {
      protected override void BuildChoices(SelectionPrompt<ViewPromptItem> prompt)
      {
         prompt.AddChoice(new ViewPromptItem("Boobs"));

         prompt.AddChoiceGroup(new ViewPromptItem("Systems"), new[] {new ViewPromptItem("Abc"), new ViewPromptItem("Efg")});
      }
   }
   
}

public class PlanetarySystemView : View<PlanetarySystem>
{
   protected override void ShowView() =>
      Console.WriteLine("Planetary System View");
}

public class SatelliteSystemView : View<SatelliteSystem>
{
   protected override void ShowView() =>
      Console.WriteLine("Satellite System View");
}
