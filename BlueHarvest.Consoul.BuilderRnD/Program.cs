Console.Title = "Blue Harvest Consoul Builder RnD";

MainMenu.Default.Show();

// var choices = new[]
// {
//    new MenuAction
//    {
//       Display = "Foo",
//       Action = null
//    },
//    new MenuAction
//    {
//       Display = "Bar",
//       Action = null
//    },
// };
//
// var prompt = new SelectionPrompt<MenuAction>()
//    .Mode(SelectionMode.Independent)
//    .PageSize(5)
//    .Title("Prompt Action Test")
//    .AddChoices(choices);
// var response = AnsiConsole.Prompt(prompt);

Console.WriteLine("Done.");
Console.ReadKey(true);

public abstract class AppMenu
{
   protected abstract string Title { get; }
   protected abstract IEnumerable<MenuItem> Items { get; }
   protected virtual SelectionMode Mode => SelectionMode.Independent;
   protected virtual int PageSize => 5;

   public virtual MenuItem Show(bool executeAction = true)
   {
      var prompt = new SelectionPrompt<MenuItem>()
         .Mode(Mode)
         .PageSize(PageSize)
         .Title(Title)
         .AddChoices(Items);

      prompt.HighlightStyle = new Style(Color.Blue, Color.LightCoral);
      
      
      var menuItem = AnsiConsole.Prompt(prompt);

      if (executeAction && menuItem?.Action != null)
      {
         menuItem?.Action.Invoke();
      }

      return menuItem;
   }
}

public class MainMenu : AppMenu
{
   public static readonly MainMenu Default = new MainMenu();

   protected override string Title => "Main Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Star Cluster",
            Action = () => { Console.WriteLine("Build star clusters."); }
         },
         new MenuItem
         {
            Display = "Planetary Systems",
            Action = () => { Console.WriteLine("Build planetary systems."); }
         },
         new MenuItem
         {
            Display = "Satellite Systems",
            Action = () => { Console.WriteLine("Build Satellite systems."); }
         },
      };
}

public class MenuItem
{
   public string? Display { get; set; }
   public Action? Action { get; set; }

   public override string? ToString() => Display;
}
