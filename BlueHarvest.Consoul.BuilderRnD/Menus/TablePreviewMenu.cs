using BlueHarvest.Consoul.BuilderRnD.Previews;

namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class TablePreviewMenu : AppMenu
{
   // public static readonly TablePreviewMenu Default = new();

   protected override string Title => "Table Builder Mneu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Star Cluster",
            Handler = () => { Preview.StarCluster.Show(); }
         },
         new MenuItem
         {
            Display = "Planetary Systems",
            Handler = () => { Preview.PlanetarySystem.Show(); }
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
