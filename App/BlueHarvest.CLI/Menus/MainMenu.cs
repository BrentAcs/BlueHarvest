using BlueHarvest.CLI.Actions;

namespace BlueHarvest.CLI.Menus;

public class MainMenu : BaseMenu
{
   public MainMenu(ILogger<MainMenu> logger, IMediator mediator)
      : base(logger, mediator)
   {
   }

   protected override string Title => "Main Menu";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.A, "List", async () => await Mediator.Send(ListClusters.Default));
   }
}
