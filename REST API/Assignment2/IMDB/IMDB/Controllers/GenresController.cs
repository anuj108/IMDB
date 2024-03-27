using IMDB.Models;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {

        private readonly IGenreService _genreService;
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        //To get all genres
        [HttpGet]
        public IActionResult GetAllGenres()
        {
            return Ok(_genreService.Get());
        }
        //To get an Genre by ID
        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            return Ok(_genreService.Get(id));
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] Genre genre)
        {
            _genreService.Create(genre);
            return Ok(_genreService.Get());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] Genre genre)
        {
            _genreService.Update(genre);
            return Ok(_genreService.Get());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _genreService.Delete(id);
            return Ok(_genreService.Get());
        }
    }
}
