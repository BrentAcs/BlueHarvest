namespace BlueHarvest.API.Controllers;

public abstract class BaseController : ControllerBase
{
   protected IMediator Mediator { get; }

   protected BaseController(IMediator mediator)
   {
      Mediator = mediator;
   }
}
