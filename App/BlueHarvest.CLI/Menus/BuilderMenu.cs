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
      AddMenuAction(ConsoleKey.D, "Drop DB", () => Mediator.Send(ResetDb.Drop));
      AddMenuAction(ConsoleKey.I, "Initialize DB", () => Mediator.Send(ResetDb.Initialize));
      AddMenuAction(ConsoleKey.R, "Reset DB", () => Mediator.Send(ResetDb.FullReset));
      AddMenuAction(ConsoleKey.L, "List", () => Mediator.Send(ListClusters.Default));
      AddMenuAction(ConsoleKey.B, "Build Cluster", () => Mediator.Send(BuildCluster.Default));
   }
}
