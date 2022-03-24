using BlueHarvest.Consoul.BuilderRnD.Menus;
using BlueHarvest.Consoul.Common;
using BlueHarvest.Core.Rnd;

namespace BlueHarvest.Consoul.BuilderRnD.Previews.Tables;

public class StarClusterTablePreview : TablePreview
{
   protected override string Header => "Star Cluster Table Preview";

   protected override void ShowPreview()
   {
      var items = new List<AnsiConSoul.IndexedPromptString>();
      items.Add((-1, "none"));

      FakeFactory.Shallow = true;
      var col = new List<StarCluster>();
      for (int i = 0; i < 20; i++)
      {
         var cluster = FakeFactory.CreateStarCluster();
         col.Add(cluster);
         items.Add((i + 1, cluster.Name));
      }

      var table = col.Build();
      AnsiConsole.Write(table);

      var result = AnsiConSoul.Prompt(items, "pick one, bitch", 4);
      AnsiConsole.WriteLine($"{result}");
   }
}

public static class AnsiConSoul
{
   public class IndexedPromptItem<T> where T : notnull
   {
      //public static readonly IndexedPromptItem<T> None = new IndexedPromptItem<T>(-1, null, "None"); 
      
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
      public IndexedPromptString(int index, string item, string? text = null) : base(index, item, text)
      {
      }
   }
   

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
