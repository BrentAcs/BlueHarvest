namespace BlueHarvest.API.Controllers;

public class ErrorController : Controller
{
   [Route("/error-dev")]
   public IActionResult HandleErrorDev([FromServices] IHostEnvironment hostEnvironment)
   {
      if (!hostEnvironment.IsDevelopment())
         return NotFound();

      var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
      return Problem(
         detail: exceptionHandlerFeature.Error.StackTrace,
         title: exceptionHandlerFeature.Error.Message);
   }

   [Route("/error")]
   public IActionResult HandleError() =>
      Problem();
}
