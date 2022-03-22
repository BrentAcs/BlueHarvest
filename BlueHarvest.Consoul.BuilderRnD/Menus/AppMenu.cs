namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public abstract class AppMenu
{
   protected abstract string Title { get; }
   protected abstract IEnumerable<MenuItem> Items { get; }
   protected virtual SelectionMode Mode => SelectionMode.Independent;
   protected virtual int PageSize => 5;
   protected virtual Style HighlightStyle => null;
   protected virtual Style DisabledStyle => null;

   public virtual void Show(bool executeAction = true)
   {
      var menuItem = MenuItem.Default;
      while(!menuItem.ExitingMenu())
      {
         AnsiConsole.Clear();
         var prompt = new SelectionPrompt<MenuItem>()
            .Mode(Mode)
            .PageSize(PageSize)
            .Title(Title)
            .HighlightStyle(HighlightStyle)
            .AddChoices(Items);
         prompt.DisabledStyle = DisabledStyle;
         
         menuItem = AnsiConsole.Prompt(prompt);
         if (executeAction && menuItem?.Handler != null)
         {
            menuItem?.Handler.Invoke();
         }
      }
   }
}
