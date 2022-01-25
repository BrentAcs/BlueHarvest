// See https://aka.ms/new-console-template for more information

// https://codeopinion.com/why-use-mediatr-3-reasons-why-and-1-reason-not/


using AutoMapper;
using BlueHarvest.Core.Actions.Cosmic;

IConfiguration configuration = new ConfigurationBuilder()
   .SetBasePath(Directory.GetCurrentDirectory())
   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
   .AddJsonFile(
      $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json",
      optional: true)
   .AddEnvironmentVariables()
   .Build();
Log.Logger = new LoggerConfiguration()
   .ReadFrom.Configuration(configuration)
   .CreateLogger();

Host.CreateDefaultBuilder()
   .ConfigureServices(ConfigureServices)
   .ConfigureServices(services => services.AddSingleton<MainMenu>())
   .Build()
   .Services
   .GetService<MainMenu>()
   ?.Execute();

static void ConfigureServices(IServiceCollection services)
{
   var assembles = new[] {Assembly.GetExecutingAssembly()};

   services
      .AddMediatR(assembles);
   //.AddSingleton<ITest, Test>();
}


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

public class MainMenu : BaseMenu
{
   public MainMenu(ILogger<MainMenu> logger, IMediator mediator)
      : base(logger, mediator)
   {
   }

   protected override string Title => "Main Menu";

   protected override void AddActions()
   {
      AddMenuAction(ConsoleKey.A, "List", () => Mediator.Send(ListClusters.Default));
      AddMenuAction(ConsoleKey.B, "Test", () => Mediator.Send(new GetAllStarClusters.Request()));
   }
}

public static class BlueHarvestConsole
{
   public static ConsoleKeyInfo PressAnyKey() => PromptUser("Press any key to continue");

   public static ConsoleKeyInfo PromptUser(string prompt)
   {
      Console.Write(prompt);
      return Console.ReadKey();
   }
}

public class ListClusters
{
   public static readonly Request Default = new();

   public class Request : IRequest
   {
   }

   public class Command : BaseCommand<Request>
   {
      public Command(IMediator mediator, IMapper mapper, ILogger<BaseCommand<Request>> logger)
         : base(mediator, mapper, logger)
      {
      }

      protected override string HandlerName => nameof(ListClusters.Command);

      protected override async Task<Unit> OnHandle(Request request, CancellationToken cancellationToken)
      {
         // ClearScreen("Listing Star Clusters...");
         int index = 0;
         var clusters = await Mediator.Send(new GetAllStarClusters.Request()).ConfigureAwait(false);
         foreach (var cluster in clusters)
         {
            Console.WriteLine($"{++index} - {cluster.Name}: {cluster.Description}");
         }

         Console.WriteLine("Listing clusters.");
         BlueHarvestConsole.PressAnyKey();

         return Unit.Value;
      }
   }
}


// public interface ITest
// {
//    void DoSomething();
// }
//
// public class Test : ITest
// {
//    public void DoSomething() => Console.WriteLine("something.");
// }
