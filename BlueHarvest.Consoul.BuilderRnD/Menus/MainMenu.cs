namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class MainMenu : AppMenu
{
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
            Handler = () => { TreePreview.Show(); }
         },
         new MenuItem
         {
            Display = "Quit",
            Action = MenuItemActions.ExitMenu
         }
      };
}
