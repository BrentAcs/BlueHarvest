using BlueHarvest.Consoul.BuilderRnD.Previews;

namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class MainMenu : AppMenu
{
   public static readonly MainMenu Default = new();

   protected override string Title => "Main Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         new MenuItem
         {
            Display = "Star Cluster",
            Handler = () => { new StarClusterPreview().Show(); }
         },
         new MenuItem
         {
            Display = "Planetary Systems",
            Handler = () => { Console.WriteLine("Build planetary systems."); }
         },
         new MenuItem
         {
            Display = "Satellite Systems",
            Handler = () => { Console.WriteLine("Build Satellite systems."); }
         },
         new MenuItem
         {
            Display = "Quit",
            Action = MenuItemActions.ExitMenu
         }
      };
}
