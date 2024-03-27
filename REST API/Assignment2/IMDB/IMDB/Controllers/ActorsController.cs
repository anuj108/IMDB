
using IMDB.Models;
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
            return Ok(_actorService.Get());
        }
        //To get an actor by ID
        [HttpGet("{id}")]
        public IActionResult GetActor(int id)
        {
            
            return Ok(_actorService.Get(id));
        }

        [HttpPost]
        public IActionResult AddActor([FromBody]Actor actor)
        {
            _actorService.Create(actor);
            var actors = _actorService.Get();
            return Ok(actors);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Actor actor)
        {
            _actorService.Update(actor);
            var actors = _actorService.Get();
            return Ok(actors);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _actorService.Delete(id);
            var actors = _actorService.Get();
            return Ok(actors);

        }
    }
}
