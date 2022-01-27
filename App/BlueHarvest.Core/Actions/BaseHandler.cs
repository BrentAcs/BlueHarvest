namespace BlueHarvest.Core.Actions;

public abstract class BaseHandler
{
   protected IMediator Mediator { get; }
   protected IMapper Mapper { get; }
   protected ILogger<BaseHandler> Logger { get; }
   
   protected BaseHandler(IMediator mediator, IMapper mapper, ILogger<BaseHandler> logger)
   {
      Mediator = mediator;
      Mapper = mapper;
      Logger = logger;
   }
   
   protected abstract string HandlerName { get; }
   
   protected virtual void LogError(Exception ex) =>
      Logger.LogError($"Exception in: '{HandlerName}'");
}
