using System.Net;
using BlueHarvest.API.Handlers.StarClusters;
using BlueHarvest.Core.Commands.Cosmic;
using BlueHarvest.Core.Responses.Cosmic;
using CreateStarCluster = BlueHarvest.Core.Commands.Cosmic.CreateStarCluster;

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
   [ProducesResponseType(StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status409Conflict)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<StarClusterResponse>> Create([FromBody] CreateStarCluster request)
   {
      Logger.LogInformation("creating star cluster.");
      if (IsValidateOnlyRequest())
      {
         Logger.LogInformation("Request was validate only, return no content.");
         return NoContent();
      }

      var (response, error) = await Mediator.Send(request, new CancellationToken(false)).ConfigureAwait(false);
      if (error != null)
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
   public async Task<ActionResult<StarClusterResponse>> GetByName([FromRoute(Name = "name")] string? name)
   {
      var (response, error) = await Mediator.Send(new GetStarCluster.Request(name), new CancellationToken(false))
         .ConfigureAwait(false);
      if (error != null)
         return Problem(error, statusCode: (int?)HttpStatusCode.InternalServerError);

      return Ok(response);
   }

   [HttpGet(Name = "GetAll")]
   [Produces("application/json")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<IEnumerable<StarClusterResponse>>> GetByName()
   {
      var (response, error) = await Mediator.Send(new GetAllStarClusters.Request(), new CancellationToken(false))
         .ConfigureAwait(false);
      if (error != null)
         return Problem(error, statusCode: (int?)HttpStatusCode.InternalServerError);

      return Ok(response);
   }
}
