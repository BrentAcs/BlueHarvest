namespace BlueHarvest.API.Controllers;

public abstract class BaseController
{
   protected IMediator Mediator { get; }

   protected BaseController(IMediator mediator)
   {
      Mediator = mediator;
   }
}
