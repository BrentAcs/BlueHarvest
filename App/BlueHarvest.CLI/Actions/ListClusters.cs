using BlueHarvest.Core.Actions.Cosmic;
using static System.Console;
using static BlueHarvest.CLI.Utils.BlueHarvestConsole;

namespace BlueHarvest.CLI.Actions;

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
         ClearScreen("Listing Star Clusters...");
         int index = 0;
         var clusters = await Mediator.Send(new GetAllStarClusters.Request()).ConfigureAwait(false);
         foreach (var cluster in clusters)
         {
            WriteLine($"{++index} - {cluster.Name}: {cluster.Description}");
         }

         PressAnyKey();

         return Unit.Value;
      }
   }
}
