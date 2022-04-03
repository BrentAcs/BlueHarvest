namespace BlueHarvest.ConSoul.BuilderRnD.Views;

public abstract class View<T>
{
   protected T? Source { get; private set; }
   protected virtual ViewPrompt<T>? ViewPrompt => null;

   protected abstract void ShowView();

   public ViewPromptItem? Show(T? source)
   {
      Source = source;
      ShowView();
      // TODO: create default ViewPromptItem's for common tasks, ie, exit, return, etc...
      ViewPromptItem? item = null;
      if (ViewPrompt != null)
      {
         item = ViewPrompt.Show(source);
         Console.WriteLine($"{item}");
         Console.ReadKey(true);
      }

      return item;
   }
}
