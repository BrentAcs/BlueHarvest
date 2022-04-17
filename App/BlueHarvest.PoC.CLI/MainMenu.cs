using BlueHarvest.PoC.CLI.Actions;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly TestFactoryActions _testFactoryActions;
   private readonly DbUtilities _dbUtilities;

   public MainMenu(ILogger<MainMenu> logger,
      TestFactoryActions testFactoryActions,
      DbUtilities dbUtilities)
   {
      _logger = logger;
      _testFactoryActions = testFactoryActions;
      _dbUtilities = dbUtilities;
   }

   public void Execute()
   {
      _logger.LogInformation("We're happy and we know it.");

      do
      {
         Console.Clear();
         var prompt = new SelectionPrompt<ActionPrompt>()
            .Title("Blue Harvest PoC CLI")
            .PageSize(20)
            .AddChoiceGroup(new ActionPrompt("Test Factories"), new[]
            {
               new ActionPrompt($"Toggle Save-To-File (current: {_testFactoryActions.SaveToFile})",
                  _testFactoryActions.ToggleSaveToFile),
               new ActionPrompt("Star Cluster", _testFactoryActions.TestClusterFactory),
               new ActionPrompt("Planetary System", _testFactoryActions.TestPlanetarySystemFactory),
               new ActionPrompt("Satellite System", _testFactoryActions.TestSatelliteSystemFactory),
               new ActionPrompt("Planetary Distance", _testFactoryActions.TestPlanetDistanceFactory),
               new ActionPrompt("Planet", _testFactoryActions.TestPlanetFactory),
            })
            .AddChoiceGroup(new ActionPrompt("Db Utilities"), new[]
            {
               new ActionPrompt("Drop & Initialize Db", _dbUtilities.DropAndInitializeDb),
               new ActionPrompt("Drop Db", _dbUtilities.DropDb),
               new ActionPrompt("Initialize Db", _dbUtilities.InitializeDb),
               new ActionPrompt("List Clusters", _dbUtilities.ListStarClusters)
            })
            .AddChoices(
               new ActionPrompt("Quit")
            );

         var item = AnsiConsole.Prompt(prompt);
         if (item.Action is null)
            break;
         item.Action?.Invoke();
      } while (true);
   }
}
