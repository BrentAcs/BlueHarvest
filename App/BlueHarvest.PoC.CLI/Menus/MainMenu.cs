using BlueHarvest.Core.Services;
using BlueHarvest.PoC.CLI.Actions;
using BlueHarvest.Shared.Models;

namespace BlueHarvest.PoC.CLI.Menus;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly IAppStateService _appStateService;
   private readonly FactoryTestAction _factoryTestAction;
   private readonly DbUtilityAction _dbUtilityAction;
   private readonly CreateStarClusterAction _createStarClusterAction;
   private readonly ListStarClustersAction _listStarClustersAction;
   private readonly ListPlanetarySystemsAction _listPlanetarySystemsAction;
   private readonly ListSatelliteSystemsAction _listSatelliteSystemsAction;

   public MainMenu(ILogger<MainMenu> logger,
      IAppStateService appStateService,
      FactoryTestAction factoryTestAction,
      DbUtilityAction dbUtilityAction,
      CreateStarClusterAction createStarClusterAction,
      ListStarClustersAction listStarClustersAction,
      ListPlanetarySystemsAction listPlanetarySystemsAction,
      ListSatelliteSystemsAction listSatelliteSystemsAction)
   {
      _logger = logger;
      _appStateService = appStateService;
      _factoryTestAction = factoryTestAction;
      _dbUtilityAction = dbUtilityAction;
      _createStarClusterAction = createStarClusterAction;
      _listStarClustersAction = listStarClustersAction;
      _listPlanetarySystemsAction = listPlanetarySystemsAction;
      _listSatelliteSystemsAction = listSatelliteSystemsAction;
   }

   public void Execute()
   {
      Console.Title = "Blue Harvest";

      RuntimeAppState.Instance = _appStateService.Get();

      do
      {
         Console.Clear();

         AnsiConsole.Write(new Rule($"[blue]Blue Harvest[/] PoC CLI").RuleStyle("grey").LeftAligned());
         AnsiConsole.MarkupLine(
            $"Selected Star Cluster: {(RuntimeAppState.Instance.HasCurrentCluster ? $"[green]{RuntimeAppState.Instance.CurrentCluster?.Name}[/]" : "[white]none[/]")}  Planetary System: {(RuntimeAppState.Instance.HasCurrentPlanetarySystem ? $"[green]{RuntimeAppState.Instance.CurrentPlanetarySystem?.Name}[/]" : "[white]none[/]")}");
         AnsiConsole.WriteLine();

         var prompt = BuildMainPrompt();

         var item = AnsiConsole.Prompt(prompt);
         if (!item.HasAction)
            break;
         item.Invoke();
      } while (true);

      _appStateService.Update(RuntimeAppState.Instance).ConfigureAwait(false).GetAwaiter().GetResult();
   }

   private SelectionPrompt<ActionPrompt> BuildMainPrompt()
   {
      var prompt = new SelectionPrompt<ActionPrompt>()
         .Title("Main Menu")
         .PageSize(20)
         .AddChoiceGroup(ActionPrompt.GroupTitle("Cluster Playground"),
            new[]
            {
               ActionPrompt.Action("Create Star Cluster", _createStarClusterAction.Execute),
               ActionPrompt.Action("List Star Clusters", _listStarClustersAction.Execute),
               ActionPrompt.Action("List Planetary Systems", _listPlanetarySystemsAction.Execute),
               ActionPrompt.Action("List Satellite Systems", _listSatelliteSystemsAction.Execute)
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
      return prompt;
   }
}
