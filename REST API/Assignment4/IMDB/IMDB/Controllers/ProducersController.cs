﻿using IMDB.CustomExceptions;
using IMDB.Domain.Request;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllProducers()
        {
            try
            {
                return Ok(await _producerService.Get());
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
        //To get an Producer by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducer(int id)
        {
            try
            {
                return Ok(await _producerService.GetById(id));
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
        public async Task<IActionResult> AddProducer([FromBody]ProducerRequest producerRequest)
        {
            try
            {
                var producerAdded = await _producerService.Create(producerRequest);
                var routeValues = new { id = producerAdded};
                return CreatedAtAction("GetProducer",routeValues,producerAdded);
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
        public async Task<IActionResult> Update(int id, [FromBody] ProducerRequest producer)
        {
            try
            {
                await _producerService.Update(id, producer);
                return Ok("PRODUCER UPDATED WITH ID: "+id);
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
                await _producerService.Delete(id);
                return Ok("PRODUCER DELETED WITH ID: "+id);
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
