
using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllActors()
        {
            try
            {
                return Ok(_actorService.Get());
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //To get an actor by ID
        [HttpGet("{id}")]
        public IActionResult GetActor([FromRoute]int id)
        {

            try
            {
                return Ok(_actorService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult AddActor(ActorRequest actorRequest)
        {
            try
            {
                var actorAdded=_actorService.Create(actorRequest);
                var routeValues = new { id = actorAdded.Id };
                return CreatedAtAction("GetActor",routeValues,actorAdded);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ActorRequest actorRequest)
        {
            try
            {
                _actorService.Update(id,actorRequest);
                return Ok("Actor with id updated "+id);
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _actorService.Delete(id);
                return Ok("Actor with id deleted "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
