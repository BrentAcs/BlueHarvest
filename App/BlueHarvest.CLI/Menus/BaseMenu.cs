namespace BlueHarvest.CLI.Menus;

public abstract class BaseMenu
{
   public ILogger<BaseMenu> Logger { get; }
   public IMediator Mediator { get; }

   protected record MenuAction
   {
      public string Name { get; set; } = "(not defined)";
      public Action? Action { get; set; }
   }

   private readonly IDictionary<ConsoleKey, MenuAction> _actions = new Dictionary<ConsoleKey, MenuAction>();

   protected BaseMenu(ILogger<BaseMenu> logger, IMediator mediator)
   {
      Logger = logger;
      Mediator = mediator;
   }

   protected abstract string Title { get; }

   protected void InitMenu()
   {
      Console.Title = $"Program - {Title}";
      ClearActions();
      AddActions();
      AddTerminateAction();
   }

   protected void AddMenuAction(ConsoleKey key, string name, Action? action) =>
      _actions.Add(key, new MenuAction {Name = name, Action = action});

   protected void ClearActions() => _actions.Clear();
   protected abstract void AddActions();

   protected virtual void AddTerminateAction() =>
      AddMenuAction(ConsoleKey.Q, "Return", null);

   public void Execute()
   {
      InitMenu();

      while (true)
      {
         Console.Clear();
         foreach (var pair in _actions)
         {
            Console.WriteLine($"{pair.Key} - {pair.Value.Name}");
         }

         var keyInfo = Console.ReadKey(true);
         if (!_actions.ContainsKey(keyInfo.Key))
            continue;

         if (_actions[ keyInfo.Key ].Action is null)
            break;

         try
         {
            _actions[ keyInfo.Key ].Action?.Invoke();
         }
         catch (Exception ex)
         {
            Logger.LogError(ex, ex?.Message);            
         }
      }
   }
}
