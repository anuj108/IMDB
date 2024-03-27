using IMDB.Models;
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
           var moviesByYear=_movieService.GetByYear(year);
            return Ok(moviesByYear);
        }
        //To get an Movie by ID
        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            return Ok(_movieService.Get(id));
        }

        [HttpPost]
        public IActionResult AddMovie([FromBody] Movie movie)
        {
            _movieService.Create(movie);
            return Ok(_movieService.Get());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Movie movie)
        {
            _movieService.Update(movie);
            return Ok(_movieService.Get());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _movieService.Delete(id);
            return Ok(_movieService.Get());
        }
    }
}
