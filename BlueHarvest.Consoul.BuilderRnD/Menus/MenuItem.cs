namespace BlueHarvest.Consoul.BuilderRnD.Menus;

public enum MenuItemActions
{
   Nothing,
   ExitMenu = 1
}

public class MenuItem
{
   public static readonly MenuItem Default = new();
   
   public string? Display { get; set; }
   public Action? Handler { get; set; }
   public MenuItemActions Action { get; set; }

   public bool ExitingMenu() => Action == MenuItemActions.ExitMenu;
   public override string? ToString() => Display;
}
