using BlueHarvest.PoC.CLI.Actions;

namespace BlueHarvest.PoC.CLI.Menus;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly FactoryTestAction _factoryTestAction;
   private readonly ClusterPlaygroundActions _clusterPlaygroundActions;
   private readonly DbUtilityAction _dbUtilityAction;

   public MainMenu(ILogger<MainMenu> logger,
      FactoryTestAction factoryTestAction,
      ClusterPlaygroundActions clusterPlaygroundActions,
      DbUtilityAction dbUtilityAction)
   {
      _logger = logger;
      _factoryTestAction = factoryTestAction;
      _clusterPlaygroundActions = clusterPlaygroundActions;
      _dbUtilityAction = dbUtilityAction;
   }

   public void Execute()
   {
      Console.Title = "Blue Harvest";
      _logger.LogInformation("We're happy and we know it.");

      do
      {
         Console.Clear();

         AnsiConsole.Write(new Rule($"[blue]Blue Harvest[/] PoC CLI").RuleStyle("grey").LeftAligned());
         AnsiConsole.MarkupLine(
            $"Selected Star Cluster: {(AppState.HasCluster ? $"[green]{AppState.Cluster?.Name}[/]" : "[white]none[/]")}");
         AnsiConsole.WriteLine();

         var prompt = new SelectionPrompt<ActionPrompt>()
            .Title("Main Menu")
            .PageSize(20)
            .AddChoiceGroup(ActionPrompt.GroupTitle("Cluster Playground"),
               new[]
               {
                  ActionPrompt.Action("Create Star Cluster", _clusterPlaygroundActions.CreateStarCluster),
                  ActionPrompt.Action("List Star Clusters", _clusterPlaygroundActions.ListStarClusters),
                  ActionPrompt.Action("List Planetary Systems", _clusterPlaygroundActions.ListPlanetarySystems)
               })
            .AddChoiceGroup(ActionPrompt.GroupTitle("Factory Tests"),
               new[]
               {
                  ActionPrompt.Action(
                     $"Toggle Save-To-File (current: [{(_factoryTestAction.SaveToFile ? "green" : "yellow")}]{_factoryTestAction.SaveToFile})[/]",
                     _factoryTestAction.ToggleSaveToFile),
                  ActionPrompt.Action("Planetary System", _factoryTestAction.TestPlanetarySystemFactory),
                  ActionPrompt.Action("Satellite System", _factoryTestAction.TestSatelliteSystemFactory),
                  ActionPrompt.Action("Planetary Distance", _factoryTestAction.TestPlanetDistanceFactory),
                  ActionPrompt.Action("Planet", _factoryTestAction.TestPlanetFactory),
                  ActionPrompt.Action("Star", _factoryTestAction.TestStarFactory)
               })
            .AddChoiceGroup(ActionPrompt.GroupTitle("Db Utilities"),
               new[]
               {
                  ActionPrompt.Action("Drop & Initialize Db", _dbUtilityAction.DropAndInitializeDb),
                  ActionPrompt.Action("Drop Db", _dbUtilityAction.DropDb),
                  ActionPrompt.Action("Initialize Db", _dbUtilityAction.InitializeDb),
               })
            .AddChoices(ActionPrompt.Quit()
            );

         var item = AnsiConsole.Prompt(prompt);
         if (!item.HasAction)
            break;
         item.Invoke();
      } while (true);
   }
}
