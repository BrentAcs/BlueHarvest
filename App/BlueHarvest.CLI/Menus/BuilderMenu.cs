using BlueHarvest.CLI.Actions;

namespace BlueHarvest.CLI.Menus;

public class BuilderMenu : BaseMenu
{
   public BuilderMenu(ILogger<BaseMenu> logger, IMediator mediator)
      : base(logger, mediator)
   {
   }

   protected override string Title => "Builder Menu";
   
   protected override void AddActions()
   { 
      AddMenuAction(ConsoleKey.A, "List", () => Mediator.Send(ListClusters.Default));
   }
}
