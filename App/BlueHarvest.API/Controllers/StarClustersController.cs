

using BlueHarvest.API.Actions.Cosmic;
using BlueHarvest.Shared.DTOs.Cosmic;

namespace BlueHarvest.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StarClustersController : BaseController
{
   public StarClustersController(IMediator mediator) : base(mediator)
   {
   }
   
   // [HttpPost(Name = "Create")]
   // [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(StarClusterResponseDto))]
   // [ProducesResponseType(StatusCodes.Status204NoContent)]
   // [ProducesResponseType(StatusCodes.Status409Conflict)]
   // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   // public async Task<IActionResult> Create([FromBody] CreateStarClusterDto dto)
   // {
   //    Logger.LogInformation("creating star cluster.");
   //    if (IsValidateOnlyRequest())
   //    {
   //       Logger.LogInformation("Request was validate only, return no content.");
   //       return NoContent();
   //    }
   //
   //    var response = await Mediator
   //       .Send(new CreateStarCluster.Request {Dto = dto}, new CancellationToken(false))
   //       .ConfigureAwait(false);
   //    return CreatedAtRoute("GetByName", new {name = response.Name}, response);
   // }
   //
   // [HttpGet("{name}", Name = "GetByName")]
   // [Produces("application/json")]
   // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StarClusterResponseDto))]
   // [ProducesResponseType(StatusCodes.Status204NoContent)]
   // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   // public async Task<IActionResult> GetByName([FromRoute(Name = "name")] string? name)
   // {
   //    var response = await Mediator
   //       .Send(new GetStarClusterByName.Request(name))
   //       .ConfigureAwait(false);
   //    if (response == null)
   //       return NoContent();
   //    return Ok(response);
   // }

   [HttpGet(Name = "GetAll")]
   [Produces("application/json")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<IEnumerable<StarClusterDto>>> GetAll()
   {
      var response = await Mediator
         .Send(new GetAllStarClusters.Request())
         .ConfigureAwait(false);
      if (!response.Data.Any())
         return NoContent();
   
      return Ok(response.Data);
   }

   [HttpGet("GenError")]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public Task<ActionResult> Error() =>
      throw new NotImplementedException();
}
