using BlueHarvest.Consoul.BuilderRnD.Previews.Tables;
using BlueHarvest.Consoul.BuilderRnD.Previews.Trees;

namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public class MainMenu : AppMenu
{
   public static void ShowMenu() =>
      new MainMenu().Show();

   protected override string Title => "Main Menu";

   protected override IEnumerable<MenuItem> Items =>
      new[]
      {
         MenuItem.Create("Cluster Browser", StarClusterBrowser.Show), MenuItem.Create("Table Previews", TablePreviewMenu.ShowMenu),
         MenuItem.Create("Tree Previews", TreePreviewMenu.ShowMenu), MenuItem.Create("Quit", null, MenuItemActions.ExitMenu)
      };
}
