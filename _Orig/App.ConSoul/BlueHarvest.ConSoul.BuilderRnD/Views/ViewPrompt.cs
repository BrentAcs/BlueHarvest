namespace BlueHarvest.ConSoul.BuilderRnD.Views;

public class ViewPromptItem
{
   public string? Display { get; set; }

   public ViewPromptItem(string? display)
   {
      Display = display;
   }

   public override string? ToString() => Display;
}

public abstract class ViewPrompt<T>
{
   protected virtual int PageSize { get; set; } = 10;
   protected virtual string Title { get; set; } = "Please make a selection.";

   protected abstract void BuildChoices(SelectionPrompt<ViewPromptItem> prompt, T? source);

   public virtual ViewPromptItem Show(T? source)
   {
      var prompt = new SelectionPrompt<ViewPromptItem>()
         .PageSize(PageSize)
         .Title(Title)
         .AddChoices();

      BuildChoices(prompt, source);

      var item = AnsiConsole.Prompt(prompt);
      return item;
   }
}
