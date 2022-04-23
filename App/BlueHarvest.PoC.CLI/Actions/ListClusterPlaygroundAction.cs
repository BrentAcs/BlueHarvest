using BlueHarvest.PoC.CLI.Menus;

namespace BlueHarvest.PoC.CLI.Actions;

public abstract class ListClusterPlaygroundAction<T> : ClusterPlaygroundAction
{
   protected class ListPromptItem : PromptItem<T>
   {
      public ListPromptItem(string display, T? data = default) : base(display, data)
      {
      }

      public ListPromptItem(string display, PageNav navDir) : base(display, default)
      {
         NavDir = navDir;
      }

      public bool CanNav => NavDir.HasValue;
      public PageNav? NavDir { get; }
   }

   protected static ListPromptItem ShowPrompt(SelectionPrompt<ListPromptItem> prompt)
   {
      int page = 1;
      bool done = false;
      return ShowPrompt(prompt, ref page, ref done);
   }

   protected static ListPromptItem ShowPrompt(SelectionPrompt<ListPromptItem> prompt, ref int page, ref bool done)
   {
      ListPromptItem item;
      item = AnsiConsole.Prompt(prompt);
      if (item.CanNav)
      {
         if (item.NavDir == PageNav.Previous)
            page--;
         if (item.NavDir == PageNav.Next)
            page++;
      }
      else
      {
         done = true;
      }

      return item;
   }
}
