using BlueHarvest.Core.Actions.Cosmic;
using BlueHarvest.Core.Builders;

namespace BlueHarvest.CLI.Actions;

public class BuildCluster
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
         ClearScreen("Build A Star Cluster.");
         try
         {
            WriteLine("1. Extra Large");
            WriteLine("2. Large");
            WriteLine("3. Medium");
            WriteLine("4. Small");
            WriteLine("5. Test (default)");
            WriteLine("Q. Cancel");

            var key = PromptUser("Select base options");
            if (key.Key == ConsoleKey.Q)
               return Unit.Value;

            StarClusterBuilderOptions options = key.Key switch
            {
               ConsoleKey.D1 => StarClusterBuilderOptions.ExtraLarge,
               ConsoleKey.D2 => StarClusterBuilderOptions.Large,
               ConsoleKey.D3 => StarClusterBuilderOptions.Medium,
               ConsoleKey.D4 => StarClusterBuilderOptions.Small,
               _ => StarClusterBuilderOptions.Test
            };

            WriteLine("Building...");
            _ = await Mediator.Send((StarClusterBuilder.Request)options);
         }
         catch (Exception ex)
         {
            WriteLine(ex);
         }
         finally
         {
            PressAnyKey();
         }

         return Unit.Value;
      }
   }
}
