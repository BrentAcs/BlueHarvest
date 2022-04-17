using BlueHarvest.PoC.CLI.Actions;

public class MainMenu
{
   private readonly ILogger<MainMenu> _logger;
   private readonly TestFactoryActions _testFactoryActions;

   public MainMenu(ILogger<MainMenu> logger, TestFactoryActions testFactoryActions)
   {
      _logger = logger;
      _testFactoryActions = testFactoryActions;
   }
   
   public void Execute()
   {
      _logger.LogInformation("We're happy and we know it.");

      do
      {
         Console.Clear();
         var prompt = new SelectionPrompt<ActionPrompt>()
            .Title("Blue Harvest PoC CLI")
            .AddChoiceGroup(new ActionPrompt("Test Factories"), new[]
            {
               new ActionPrompt($"Toggle Save-To-File (current: {_testFactoryActions.SaveToFile})", _testFactoryActions.ToggleSaveToFile), 
               new ActionPrompt("Star Cluster", _testFactoryActions.TestClusterFactory),
               new ActionPrompt("Planetary System", _testFactoryActions.TestPlanetarySystemFactory),
               new ActionPrompt("Satellite System", _testFactoryActions.TestSatelliteSystemFactory),
               new ActionPrompt("Planetary Distance", _testFactoryActions.TestPlanetDistanceFactory),
               new ActionPrompt("Planet", _testFactoryActions.TestPlanetFactory),
            })
            .AddChoices(
               new ActionPrompt("Quit")
            );

         //IEnumerable<IMongoRepository> mongoRepos
         
         var item = AnsiConsole.Prompt(prompt);
         if (item.Action is null)
            break;
         item.Action?.Invoke();
      } while (true);
   }
}
