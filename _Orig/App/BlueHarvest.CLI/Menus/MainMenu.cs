namespace BlueHarvest.CLI.Menus;

public class MainMenu : BaseMenu
{
   private readonly BuilderMenu _builderMenu;

   public MainMenu(ILogger<MainMenu> logger, IMediator mediator, BuilderMenu builderMenu)
      : base(logger, mediator)
   {
      _builderMenu = builderMenu;
   }

   protected override string Title => "Main Menu";

   protected override void AddTerminateAction() =>
      AddMenuAction(ConsoleKey.Q, "Quit", null);
   
   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.B, "Builder", () => _builderMenu.Execute());
   }
}
