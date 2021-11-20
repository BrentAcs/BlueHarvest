using BlueHarvest.Core.Models.Cosmic;
using BlueHarvest.Core.Storage.Repos;

namespace BlueHarvest.API.Controllers;

// Ref: https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio
//      https://stoplight.io/blog/crud-api-design/
//      https://stackoverflow.com/questions/37839278/asp-net-core-rc2-web-api-post-when-to-use-create-createdataction-vs-created
//      https://code-maze.com/cqrs-mediatr-in-aspnet-core/
//      https://www.vinaysahni.com/best-practices-for-a-pragmatic-restful-api

// API	                       Description	               Request body	  Response body
// POST   /api/persons	        Add a Person	               Person	        Person
// GET    /api/persons/{id}	  Get a Person by ID	         None	           Person
// GET    /api/persons	        Get all Persons	            None	           Array of Persons
// PUT    /api/persons/{id}	  Update an existing Person   Person	        None
// DELETE /api/persons/{id}    Delete an Person    	      None	           None

// NOTE: Yes, is code smell, but want to keep sample for now. Will refactor later

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StarClusterController : Controller
{
   private readonly IStarClusterRepo _repo;
   private readonly ILogger<StarClusterController> _logger;

   public StarClusterController(IStarClusterRepo repo,
      ILogger<StarClusterController> logger)
   {
      _repo = repo;
      _logger = logger;
   }

   [HttpGet("{id}", Name = "GetById")]
   [Produces("application/json")]
   [ProducesResponseType(StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   [ProducesResponseType(StatusCodes.Status500InternalServerError)]
   public async Task<ActionResult<StarCluster>> GetById([FromRoute(Name = "id")] int id)
   {
      // var response = await Mediator.Send((PersonsGetById.Request)id).ConfigureAwait(false);
      // if (response == null)
      //    return NotFound(id);

      var response = _repo.All().FirstOrDefault();

      return Ok(response);
   }
}
