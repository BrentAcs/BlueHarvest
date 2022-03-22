using Spectre.Console;

Console.Title = "Blue Harvest Consoul Builder RnD";

var choices = new[]
{
   new MenuAction
   {
      Display = "Foo",
      Action = null
   },
   new MenuAction
   {
      Display = "Bar",
      Action = null
   },
};

var prompt = new SelectionPrompt<MenuAction>()
   .Mode(SelectionMode.Independent)
   .PageSize(5)
   .Title("Prompt Action Test")
   .AddChoices(choices);
var response = AnsiConsole.Prompt(prompt);

Console.WriteLine("Done.");
Console.ReadKey(true);

public abstract class AppMenu
{
//   public 
   
}

public class MainMenu : AppMenu
{
   
}

public class MenuAction
{
   public string? Display { get; set; }
   public Action? Action { get; set; }

   public override string? ToString() => Display;
}
