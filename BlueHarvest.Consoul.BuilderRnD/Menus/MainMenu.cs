namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class MainMenu : AppMenu
{
   public static readonly MainMenu Default = new();

   protected override string Title => "Main Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         MenuItem.Create("Cluster Browser", StarClusterBrowser.Show), 
         MenuItem.Create("Table Previews", () =>
         {
            TablePreviewMenu.Default.Show();
         }),
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
