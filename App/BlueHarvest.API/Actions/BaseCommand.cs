namespace BlueHarvest.API.Actions;

public abstract class BaseCommand<TCmd> : BaseHandler, IRequestHandler<TCmd>
   where TCmd : IRequest
{
   protected BaseCommand(ILogger<BaseCommand<TCmd>> logger)
      : base(logger)
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
