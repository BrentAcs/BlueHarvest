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

   private const string ValidateOnly = "ValidateOnly";
   protected bool IsValidateOnlyRequest()
   {
      bool validateOnly = false;
      if (Request.Headers.ContainsKey(ValidateOnly))
      {
         _ = bool.TryParse(Request.Headers[ValidateOnly], out validateOnly);
      }

      return validateOnly;
   }
}
