﻿// See https://aka.ms/new-console-template for more information

// https://codeopinion.com/why-use-mediatr-3-reasons-why-and-1-reason-not/

Host.CreateDefaultBuilder()
   .ConfigureServices(ConfigureServices)
   .ConfigureServices(services => services.AddSingleton<MainMenu>())
   .Build()
   .Services
   .GetService<MainMenu>()
   ?.Execute();

static void ConfigureServices(IServiceCollection services)
{
   var assembles = new[] { Assembly.GetExecutingAssembly() };

   services
      .AddMediatR(assembles)
      .AddSingleton<ITest, Test>();
}

                                     
public abstract class BaseMenu
{
   public IMediator Mediator { get; }

   protected record MenuAction
   {
      public string Name { get; set; } = "(not defined)";
      public Action? Action { get; set; }
   }

   private readonly IDictionary<ConsoleKey, MenuAction> _actions = new Dictionary<ConsoleKey, MenuAction>();

   protected BaseMenu(IMediator mediator)
   {
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
      AddMenuAction(ConsoleKey.Q, "Quit/Return", null);

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
         {
            continue;
         }

         if (_actions[ keyInfo.Key ].Action is null)
            break;

         _actions[ keyInfo.Key ].Action?.Invoke();
      }
   }
}

public class MainMenu : BaseMenu
{
   public MainMenu(IMediator mediator)
      : base(mediator)
   {
   }

   protected override string Title => "Main Menu";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.A, "List", () => Mediator.Send(new ListClusters()));
      
   }
}

public class ListClusters : IRequest, IRequestHandler<ListClusters>
{
   public Task<Unit> Handle(ListClusters request, CancellationToken cancellationToken)
   {
      Console.WriteLine("Listing clusters.");
      Console.ReadKey();

      return Unit.Task;
   }
}


public interface ITest
{
   void DoSomething();
}

public class Test : ITest
{
   public void DoSomething() => Console.WriteLine("something.");
}


