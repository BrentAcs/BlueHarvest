using static System.Console;

namespace BlueHarvest.Server.CLI.Services;

internal interface IBaseService
{
   void ProcessMenu();
}

internal abstract class BaseService : IBaseService
{
   protected record MenuAction
   {
      public string Name { get; set; } = string.Empty;
      public Action? Action { get; set; }
   }

   private readonly IDictionary<ConsoleKey, MenuAction> _actions = new Dictionary<ConsoleKey, MenuAction>();
   private readonly IConfiguration _configuration;
   private readonly ILogger<BaseService> _logger;

   protected BaseService(
      IConfiguration configuration,
      ILogger<BaseService> logger)
   {
      _configuration = configuration;
      _logger = logger;
   }

   protected IConfiguration Configuration => _configuration;
   protected ILogger<BaseService> Logger => _logger;
   protected abstract string Title { get; }

   protected virtual void AddTerminateAction() =>
      AddMenuAction(ConsoleKey.Q, "Quit/Return", null);

   protected void InitMenu()
   {
      ClearActions();
      AddActions();
      AddTerminateAction();
   }

   protected void ClearActions() => _actions.Clear();
   protected abstract void AddActions();

   protected void AddMenuAction(ConsoleKey key, string name, Action? action) =>
      _actions.Add(key, new MenuAction {Name = name, Action = action});

   public void ProcessMenu()
   {
      InitMenu();
      while (true)
      {
         Clear();
         WriteLine(Title);
         foreach (var pair in _actions)
         {
            WriteLine($"{GetKeyDisplay(pair.Key)} - {pair.Value.Name}");
         }

         var keyInfo = ReadKey(true);
         if (!_actions.ContainsKey(keyInfo.Key))
         {
            continue;
         }

         if (_actions[keyInfo.Key].Action is null)
            break;

         _actions[keyInfo.Key].Action?.Invoke();
      }
   }

   private static string GetKeyDisplay(ConsoleKey key)
   {
      if (key is >= ConsoleKey.D0 and <= ConsoleKey.D9)
         return $"{(int)key - (int)ConsoleKey.D0}";
      
      return $"{key}";
   }

   protected void ClearScreen(string? subTitle=null)
   {
      Clear();
      if (!string.IsNullOrEmpty(subTitle))
      {
         WriteLine($"{subTitle}");
      }
   }

   protected void ShowContinue()
   {
      WriteLine();
      WriteLine("press any key to continue");
      ReadKey(true);
   }

   protected ConsoleKey ShowPrompt(string prompt, ConsoleKey defaultKey=ConsoleKey.Q)
   {
      Write($"{prompt}: ");
      var input = ReadKey();
      WriteLine();
      return input.Key;
   }
}
