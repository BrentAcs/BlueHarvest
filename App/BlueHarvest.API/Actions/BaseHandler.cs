namespace BlueHarvest.API.Actions;

public abstract class BaseHandler
{
   protected ILogger<BaseHandler> Logger { get; }
   
   protected BaseHandler(ILogger<BaseHandler> logger)
   {
      Logger = logger;
   }
   
   protected abstract string HandlerName { get; }
   
   protected virtual void LogError(Exception ex) =>
      Logger.LogError($"Exception in: '{HandlerName}'");
}
