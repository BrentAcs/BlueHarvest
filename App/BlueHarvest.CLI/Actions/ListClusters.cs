using BlueHarvest.Core.Actions.Cosmic;
using static System.Console;
using static BlueHarvest.CLI.Utils.BlueHarvestConsole;

namespace BlueHarvest.CLI.Actions;

// https://codeopinion.com/fat-controller-cqrs-diet-command-pipeline/

public class ListClusters
{
   public static readonly Request Default = new();

   public class Request : IRequest
   {
   }

   public class Command : BaseCommand<Request>
   {
      public Command(IMediator mediator, ILogger<BaseCommand<Request>> logger)
         : base(mediator, logger)
      {
      }

      protected override string HandlerName => nameof(Command);

      protected override async Task<Unit> OnHandle(Request request, CancellationToken cancellationToken)
      {
         ClearScreen("Listing Star Clusters...");
         int index = 0;
         var clusters = await Mediator.Send( GetAllStarClusters.Default).ConfigureAwait(false);
         foreach (var cluster in clusters)
         {
            WriteLine($"{++index} - {cluster.Name}: {cluster.Description}");
         }

         PressAnyKey();

         return Unit.Value;
      }
   }
}
