namespace BlueHarvest.PoC.CLI.Menus;

public class PromptItem<T>
{
   public PromptItem(string display, T? data=default)
   {
      Display = display;
      Data = data;
   }

   public override string ToString() => Display;

   public string Display { get;  }
   public T? Data { get; }
}
