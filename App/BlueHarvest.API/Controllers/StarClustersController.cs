using System.Net;
using BlueHarvest.API.DTOs.Cosmic;
using BlueHarvest.API.Handlers.StarClusters;
using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StarClustersController : BaseController
{
   public StarClustersController(IMediator mediator,
      ILogger<StarClustersController> logger) : base(mediator, logger)
   {
   }

   [HttpPost(Name = "Create")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status409Conflict)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<StarClusterResponseDto>> Create([FromBody] CreateStarClusterRequestDto request)
   {
      Logger.LogInformation("creating star cluster.");

      var (response, error) = await Mediator.Send(request, new CancellationToken(false)).ConfigureAwait(false);
      if (response is null)
      {
         return Problem(error, statusCode: (int?)HttpStatusCode.Conflict);
      }

      return CreatedAtRoute("GetByName", new {name = response.Name}, response);
   }

   [HttpGet("{name}", Name = "GetByName")]
   [Produces("application/json")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<StarClusterResponseDto>> GetByName([FromRoute(Name = "name")] string? name)
   {
      var (response, error) = await Mediator.Send(new GetStarCluster.Request(name), new CancellationToken(false))
         .ConfigureAwait(false);
      if (error != null)
         return Problem(error, statusCode: (int?)HttpStatusCode.InternalServerError);

      return Ok(response);
   }
}
