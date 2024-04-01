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
            try
            {
                return Ok(_genreService.Get());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //To get an Genre by ID
        [HttpGet("{id}")]
        public IActionResult GetGenre(int id)
        {
            try
            {
                return Ok(_genreService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] GenreRequest genreRequest)
        {
            try
            {
                var addedGenre = _genreService.Create(genreRequest);
                var routeValues = new { id = addedGenre.Id };
                return CreatedAtAction("GetGenre", routeValues, addedGenre);
            }
            
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute ]int id,[FromBody] GenreRequest genreRequest)
        {
            try
            {
                _genreService.Update(id,genreRequest);
                return Ok("GENRE UPDATED WITH ID "+id);
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
                _genreService.Delete(id);
                return Ok("GENRE DELETED WITH ID "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
