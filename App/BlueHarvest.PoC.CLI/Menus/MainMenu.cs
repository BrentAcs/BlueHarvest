using BlueHarvest.PoC.CLI.Actions;

namespace BlueHarvest.PoC.CLI.Menus;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly FactoryTestAction _factoryTestAction;
   private readonly DbUtilityAction _dbUtilityAction;
   private readonly CreateStarClusterAction _createStarClusterAction;
   private readonly ListStarClustersAction _listStarClustersAction;
   private readonly ListPlanetarySystemsAction _listPlanetarySystemsAction;

   public MainMenu(ILogger<MainMenu> logger,
      FactoryTestAction factoryTestAction,
      DbUtilityAction dbUtilityAction,
      CreateStarClusterAction createStarClusterAction,
      ListStarClustersAction listStarClustersAction,
      ListPlanetarySystemsAction listPlanetarySystemsAction)
   {
      _logger = logger;
      _factoryTestAction = factoryTestAction;
      _dbUtilityAction = dbUtilityAction;
      _createStarClusterAction = createStarClusterAction;
      _listStarClustersAction = listStarClustersAction;
      _listPlanetarySystemsAction = listPlanetarySystemsAction;
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
                  ActionPrompt.Action("Create Star Cluster", _createStarClusterAction.Execute),
                  ActionPrompt.Action("List Star Clusters", _listStarClustersAction.Execute),
                  ActionPrompt.Action("List Planetary Systems", _listPlanetarySystemsAction.Execute)
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
