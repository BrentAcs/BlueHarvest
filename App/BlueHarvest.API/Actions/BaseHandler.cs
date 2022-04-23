namespace BlueHarvest.API.Actions;

public abstract class BaseHandler
{
   protected IMediator Mediator { get; }
   protected ILogger<BaseHandler> Logger { get; }
   
   protected BaseHandler(IMediator mediator, ILogger<BaseHandler> logger)
   {
      Mediator = mediator;
      Logger = logger;
   }
   
   protected abstract string HandlerName { get; }
   
   protected virtual void LogError(Exception ex) =>
      Logger.LogError($"Exception in: '{HandlerName}'");
}
