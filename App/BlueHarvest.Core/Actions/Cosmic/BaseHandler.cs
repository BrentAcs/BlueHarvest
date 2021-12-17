namespace BlueHarvest.Core.Actions.Cosmic;

public abstract class BaseHandler<TReq, TRes> : IRequestHandler<TReq, TRes>
   where TReq : IRequest<TRes>
{
   protected IMediator Mediator { get; }
   protected IMapper Mapper { get; }
   protected ILogger<BaseHandler<TReq, TRes>> Logger { get; }

   protected BaseHandler(IMediator mediator, IMapper mapper, ILogger<BaseHandler<TReq, TRes>> logger)
   {
      Mediator = mediator;
      Mapper = mapper;
      Logger = logger;
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

   protected abstract string HandlerName { get; }

   protected virtual void LogError(Exception ex) =>
      Logger.LogError($"Exception in: '{HandlerName}'");

   protected abstract Task<TRes> OnHandle(TReq request, CancellationToken cancellationToken);
}
