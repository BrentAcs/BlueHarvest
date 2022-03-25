using BlueHarvest.ConSoul.BuilderRnD.Previews;

namespace BlueHarvest.ConSoul.BuilderRnD.Menus;

public class TreePreviewMenu : AppMenu
{
   public static void ShowMenu() => ((AppMenu)new TreePreviewMenu()).Show();

   protected override string Title => "Tree Builder Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Star Cluster",
            Handler = () => { Preview.StarClusterTree.Show(); }
         },
         new MenuItem
         {
            Display = "Planetary Systems",
            //Handler = () => { Preview.PlanetarySystemTable.Show(); }
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
