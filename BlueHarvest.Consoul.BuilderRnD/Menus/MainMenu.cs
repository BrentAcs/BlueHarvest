namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class MainMenu : AppMenu
{
   // public static readonly MainMenu Default = new();

   protected override string Title => "Main Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Table Previews",
            Handler = () => { TablePreview.Show(); }
         },
         new MenuItem
         {
            Display = "Tree Previews",
            //Handler = () => { TablePreviewMenu.Default.Show(); }
         },
         new MenuItem
         {
            Display = "Quit",
            Action = MenuItemActions.ExitMenu
         }
      };
}
