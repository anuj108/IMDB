using IMDB.Models;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducersController : ControllerBase
    {
        //To get all Producers
        private readonly IProducerService _producerService;
        public ProducersController(IProducerService producerService)
        {
            _producerService=producerService;
        }
        [HttpGet]
        public IActionResult GetAllProducers()
        {
            return Ok(_producerService.Get());
        }
        //To get an Producer by ID
        [HttpGet("{id}")]
        public IActionResult GetProducer(int id)
        {
            return Ok(_producerService.Get(id));
        }

        [HttpPost]
        public IActionResult AddProducer([FromBody] Producer producer)
        {
            _producerService.Create(producer);
            return Ok(_producerService.Get());
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Producer producer)
        {
            _producerService.Update(producer);
            return Ok(_producerService.Get());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _producerService.Delete(id);
            return Ok(_producerService.Get());
        }
    }
}
