public class ActionPrompt
{
   public ActionPrompt(string display, Action? action = null)
   {
      Display = display;
      Action = action;
   }

   public string Display { get; set; }
   public Action? Action { get; set; }

   public override string ToString() => Display;
}
