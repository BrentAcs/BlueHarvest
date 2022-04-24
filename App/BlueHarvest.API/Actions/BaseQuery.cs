namespace BlueHarvest.API.Actions;

public abstract class BaseQuery<TReq, TRes> : BaseHandler, IRequestHandler<TReq, TRes>
   where TReq : IRequest<TRes>
{
   protected BaseQuery(ILogger<BaseQuery<TReq, TRes>> logger)
      : base(logger)
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
