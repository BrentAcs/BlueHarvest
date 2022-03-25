using BlueHarvest.Consoul.BuilderRnD.Previews;

namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class TablePreviewMenu : AppMenu
{
   public static void ShowMenu() =>
      new TablePreviewMenu().Show();

   protected override string Title => "Table Builder Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Star Cluster",
            Handler = () => { Preview.StarClusterTable.Show(); }
         },
         new MenuItem
         {
            Display = "Planetary Systems",
            Handler = () => { Preview.PlanetarySystemTable.Show(); }
         },
         new MenuItem
         {
            Display = "Satellite Systems",
            Handler = () => { Console.WriteLine("Build Satellite systems."); }
         },
         new MenuItem
         {
            Display = "Return",
            Action = MenuItemActions.ExitMenu
         }
      };
}
