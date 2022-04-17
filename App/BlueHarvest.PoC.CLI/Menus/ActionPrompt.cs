namespace BlueHarvest.PoC.CLI.Menus;

public class ActionPrompt
{
   private ActionPrompt(string display="") {
      Display = display;
   }

   public static ActionPrompt GroupTitle(string display) => new() {Display = display};

   public static ActionPrompt Action(string display, Action? action) => new()
   {
      Display = display,
      _action = action
   };

   public static ActionPrompt Action(string display, Func<Task>? funcAsync) => new()
   {
      Display = display,
      _funcAsync = funcAsync
   };

   public static ActionPrompt Quit() => new() {Display = "Quit"};


   private string Display { get; set; }
   private Action? _action;
   private Func<Task>? _funcAsync;

   public override string ToString() => Display;

   public bool HasAction => _action is not null || _funcAsync is not null;

   public void Invoke()
   {
      if (!HasAction)
         return;

      try
      {
         if (_action is not null)
         {
            _action.Invoke();
         }

         if (_funcAsync is not null)
         {
            (_funcAsync?.Invoke())?.Wait();
         }
      }
      catch (Exception ex)
      {
         AnsiConsole.WriteException(ex);
         Console.WriteLine("Press any key to continue.");
         Console.ReadKey(true);
      }
   }
}
