using System.Net;
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
   public async Task<ActionResult<CreateStarCluster.Response>> Create([FromBody] CreateStarCluster.Request request)
   {
      Logger.LogInformation("creating star cluster.");

      var (response, error) = await Mediator.Send(request, new CancellationToken(false)).ConfigureAwait(false);
      if (response is null)
      {
         return Problem(error, statusCode: (int?)HttpStatusCode.Conflict);
      }

      // TODO: Start here, this was proof of concept. Need to fix up for correct route.
      return CreatedAtRoute("GetById", new {id = "123"}, response);
   }

   [HttpGet("{id}", Name = "GetById")]
   [Produces("application/json")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<StarCluster>> GetById([FromRoute(Name = "id")] int id)
   {
      Logger.LogInformation("getting star cluster.");

      // var response = await Mediator.Send((PersonsGetById.Request)id).ConfigureAwait(false);
      // if (response == null)
      //    return NotFound(id);

      // var response = _repo.All().FirstOrDefault();

      var response = new StarCluster {Name = "test name", Description = "test description"};
      return Ok(response);
   }
}
