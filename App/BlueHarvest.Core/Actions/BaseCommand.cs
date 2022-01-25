namespace BlueHarvest.Core.Actions.Cosmic;

public abstract class BaseCommand<TCmd> : BaseHandler, IRequestHandler<TCmd>
   where TCmd : IRequest
{
   protected BaseCommand(IMediator mediator, IMapper mapper, ILogger<BaseCommand<TCmd>> logger)
      : base(mediator, mapper, logger)
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
