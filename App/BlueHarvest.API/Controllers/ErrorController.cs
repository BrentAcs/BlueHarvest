using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BlueHarvest.API.Controllers;

public class ErrorController : Controller
{
   [ApiExplorerSettings(IgnoreApi = true)]
   [Route("/error-dev")]
   [HttpGet]
   public IActionResult HandleErrorDev([FromServices] IHostEnvironment hostEnvironment)
   {
      if (!hostEnvironment.IsDevelopment())
         return NotFound();

      var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
      return Problem(
         detail: exceptionHandlerFeature.Error.StackTrace,
         title: exceptionHandlerFeature.Error.Message);
   }

   [ApiExplorerSettings(IgnoreApi = true)]
   [Route("/error")]
   [HttpGet]
   public IActionResult HandleError() =>
      Problem();
}
