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
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //To get all Movies

        [HttpGet("")]
  
        public IActionResult GetAllMoviesByYear([FromQuery] int year)
        {
            try
            {
                return Ok(_movieService.GetByYear(year));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        //To get an Movie by ID
        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            try
            {
                return Ok(_movieService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] MovieRequest movieRequest)
        {
            try
            {
                var addedMovie=_movieService.Create(movieRequest);
                var routeValues=new {id=addedMovie.Id};
                return CreatedAtAction("GetMovie",routeValues,addedMovie);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] MovieRequest movieRequest)
        {
            try
            {
                _movieService.Update(id, movieRequest);
                return Ok("Movie updated with id "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            try
            {
                _movieService.Delete(id);
                return Ok("Movie deleted with id "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
