using BlueHarvest.PoC.CLI.Actions;

namespace BlueHarvest.PoC.CLI.Menus;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly FactoryTestActions _factoryTestActions;
   private readonly ClusterPlaygroundActions _clusterPlaygroundActions;
   private readonly DbUtilityActions _dbUtilityActions;

   public MainMenu(ILogger<MainMenu> logger,
      FactoryTestActions factoryTestActions,
      ClusterPlaygroundActions clusterPlaygroundActions,
      DbUtilityActions dbUtilityActions)
   {
      _logger = logger;
      _factoryTestActions = factoryTestActions;
      _clusterPlaygroundActions = clusterPlaygroundActions;
      _dbUtilityActions = dbUtilityActions;
   }

   public void Execute()
   {
      Console.Title = "Blue Harvest";
      _logger.LogInformation("We're happy and we know it.");

      do
      {
         Console.Clear();
         var prompt = new SelectionPrompt<ActionPrompt>()
            .Title("[blue]Blue Harvest[/] PoC CLI")
            .PageSize(20)
            .AddChoiceGroup(ActionPrompt.GroupTitle("Cluster Playground"),
               new []
               {
                  ActionPrompt.Action("Create Star Cluster", _clusterPlaygroundActions.CreateStarCluster),
                  ActionPrompt.Action("List Star Clusters", _clusterPlaygroundActions.ListStarClusters)
               })
            .AddChoiceGroup(ActionPrompt.GroupTitle("Factory Tests"),
               new[]
               {
                  ActionPrompt.Action(
                     $"Toggle Save-To-File (current: [{(_factoryTestActions.SaveToFile ? "green" : "yellow")}]{_factoryTestActions.SaveToFile})[/]",
                     _factoryTestActions.ToggleSaveToFile),
                  ActionPrompt.Action("Planetary System", _factoryTestActions.TestPlanetarySystemFactory),
                  ActionPrompt.Action("Satellite System", _factoryTestActions.TestSatelliteSystemFactory),
                  ActionPrompt.Action("Planetary Distance", _factoryTestActions.TestPlanetDistanceFactory),
                  ActionPrompt.Action("Planet", _factoryTestActions.TestPlanetFactory),
                  ActionPrompt.Action("Star", _factoryTestActions.TestStarFactory)
               })
            .AddChoiceGroup(ActionPrompt.GroupTitle("Db Utilities"),
               new[]
               {
                  ActionPrompt.Action("Drop & Initialize Db", _dbUtilityActions.DropAndInitializeDb),
                  ActionPrompt.Action("Drop Db", _dbUtilityActions.DropDb),
                  ActionPrompt.Action("Initialize Db", _dbUtilityActions.InitializeDb),
                  //new ActionPrompt("List Clusters", _dbUtilities.ListStarClusters)
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
