
using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActorsController:ControllerBase
    {
        private readonly IActorService _actorService;
        public ActorsController(IActorService actorService)
        {
            _actorService= actorService;
        }
        //To get all actors
        [HttpGet]
        public async Task<IActionResult> GetAllActors()
        {
            try
            {
                return Ok( await _actorService.Get());
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        //To get an actor by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor([FromRoute]int id)
        {

            try
            {
                return Ok(await _actorService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddActor([FromBody]ActorRequest actorRequest)
        {
            try
            {
                var actorAdded=await _actorService.Create(actorRequest);
                var routeValues = new { Id = actorAdded };
                
                return CreatedAtAction("GetActor",routeValues,actorAdded);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActorRequest actorRequest)
        {
            try
            {
                await _actorService.Update(id,actorRequest);
                return Ok("ACTOR WITH THE GIVEN ID UPDATED: "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _actorService.Delete(id);
                return Ok("ACTOR WITH THE GIVEN ID DELETED: "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }


        }
    }
}
