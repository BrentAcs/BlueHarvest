namespace BlueHarvest.API.Actions;

public abstract class BaseCommand<TCmd> : BaseHandler, IRequestHandler<TCmd>
   where TCmd : IRequest
{
   protected BaseCommand(IMediator mediator, ILogger<BaseCommand<TCmd>> logger)
      : base(mediator, logger)
   {
   }

   public async Task<Unit> Handle(TCmd command, CancellationToken cancellationToken)
   {
      try
      {
         return await OnHandle(command, cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
         LogError(ex);
         throw;
      }
   }

   protected abstract Task<Unit> OnHandle(TCmd request, CancellationToken cancellationToken);
}
