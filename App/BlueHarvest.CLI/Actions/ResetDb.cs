using BlueHarvest.Core.Actions.Cosmic;
using BlueHarvest.Core.Extensions;
using BlueHarvest.Core.Storage;

namespace BlueHarvest.CLI.Actions;

public class ResetDb
{
   public static readonly Request Drop = new Request {DropDb = true, InitializeDb = false};
   public static readonly Request Initialize = new Request {DropDb = false, InitializeDb = true};
   public static readonly Request FullReset = new Request {DropDb = true, InitializeDb = true};

   public class Request : IRequest
   {
      public bool DropDb { get; set; }
      public bool InitializeDb { get; set; }
   }

   public class Command : BaseCommand<Request>
   {
      private readonly IMongoContext _mongoContext;
      private readonly IEnumerable<IMongoRepository> _mongoRepos;

      public Command(
         IMediator mediator,
         IMapper mapper, 
         ILogger<BaseCommand<Request>> logger,
         IMongoContext mongoContext,
         IEnumerable<IMongoRepository> mongoRepos)
         : base(mediator, mapper, logger)
      {
         _mongoContext = mongoContext;
         _mongoRepos = mongoRepos;
      }

      protected override string HandlerName => nameof(Command);

      protected override async Task<Unit> OnHandle(Request request, CancellationToken cancellationToken)
      {
         if (request.DropDb)
         {
            ClearScreen("Dropping DB");
            Write("This is destructive. Are you sure? Type 'DEL' to delete: ");
            var line = ReadLine();
            if (line!.Equals("DEL"))
            {
               WriteLine("Deleting...");
               _mongoContext.Client.DropDatabaseAsync(_mongoContext.Settings.DatabaseName).ConfigureAwait(false);
            }
         }

         if (request.InitializeDb)
         {
            WriteLine("Initializing...");
            Task.WaitAll(_mongoRepos.InitializeAllIndexesAsync());
            WriteLine("Seeding...");
            Task.WaitAll(_mongoRepos.SeedAllDataAsync());
         }
         
         PressAnyKey();

         return Unit.Value;
      }
   }
}
