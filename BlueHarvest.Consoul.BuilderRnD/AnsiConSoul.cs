namespace BlueHarvest.Consoul.BuilderRnD;

public static class AnsiConSoul
{
   public static T Prompt<T>(IEnumerable<T> items, string title, int pageSize, Style? highlightStyle = null, Style? disabledStyle = null)
   {
      var prompt = new SelectionPrompt<T>()
         .PageSize(pageSize)
         .Title(title)
         .HighlightStyle(highlightStyle)
         .AddChoices(items);
      prompt.DisabledStyle = disabledStyle;

      T result = AnsiConsole.Prompt(prompt);
      return result;
   }
}

public class IndexedPromptItem<T> where T : notnull
{
   public int Index { get; set; }
   public string? Text { get; set; }
   public T? Item { get; set; }

   public IndexedPromptItem(int index, T item, string? text = null)
   {
      Index = index;
      Item = item;
      Text = text ?? item.ToString();
   }

   public static IndexedPromptItem<T> Create(int index, T item, string? text = null) => new(index, item, text);

   public override string? ToString() => Text;
}

public class IndexedPromptString : IndexedPromptItem<string>
{
   public static readonly IndexedPromptString None = new(-1, null, "None"); 

   public static IndexedPromptString Create(int index, string? text) => new(index, null, text);
   
   public IndexedPromptString(int index, string item, string? text = null) : base(index, item, text)
   {
   }
}
