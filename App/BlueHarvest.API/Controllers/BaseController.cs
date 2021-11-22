namespace BlueHarvest.API.Controllers;

public abstract class BaseController : Controller
{
   protected IMediator Mediator { get; }
   protected ILogger<BaseController> Logger { get; }

   protected BaseController(IMediator mediator,
      ILogger<BaseController> logger)
   {
      Mediator = mediator;
      Logger = logger;
   }
}
