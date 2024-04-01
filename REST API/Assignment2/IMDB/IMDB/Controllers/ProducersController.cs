using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
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
            try
            {
                return Ok(_producerService.Get());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        //To get an Producer by ID
        [HttpGet("{id}")]
        public IActionResult GetProducer(int id)
        {
            try
            {
                return Ok(_producerService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPost]
        public IActionResult AddProducer([FromBody]ProducerRequest producerRequest)
        {
            try
            {
                var producerAdded = _producerService.Create(producerRequest);
                var routeValues = new { id = producerAdded.Id };
                return CreatedAtAction("GetProducer",routeValues,producerAdded);
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProducerRequest producer)
        {
            try
            {
                _producerService.Update(id, producer);
                return Ok("Producer with id updated "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _producerService.Delete(id);
                return Ok("Producer with id deleted "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
