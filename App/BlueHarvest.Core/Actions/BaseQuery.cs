namespace BlueHarvest.Core.Actions;

public abstract class BaseQuery<TReq, TRes> : BaseHandler, IRequestHandler<TReq, TRes>
   where TReq : IRequest<TRes>
{
   protected BaseQuery(IMediator mediator, ILogger<BaseQuery<TReq, TRes>> logger)
      : base(mediator, logger)
   {
   }

   public async Task<TRes> Handle(TReq request, CancellationToken cancellationToken)
   {
      try
      {
         return await OnHandle(request, cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
         LogError(ex);
         throw;
      }
   }

   protected abstract Task<TRes> OnHandle(TReq request, CancellationToken cancellationToken);
}
