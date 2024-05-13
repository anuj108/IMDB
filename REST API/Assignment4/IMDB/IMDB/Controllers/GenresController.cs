using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                return Ok(await _genreService.Get());
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
        //To get an Genre by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            try
            {
                return Ok(await _genreService.Get(id));
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
        public async Task<IActionResult> AddGenre([FromBody] GenreRequest genreRequest)
        {
            try
            {
                var addedGenre =await _genreService.Create(genreRequest);
                var routeValues = new { id = addedGenre };
                return CreatedAtAction("GetGenre", routeValues, addedGenre);
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
        public async Task<IActionResult> Update([FromRoute ]int id,[FromBody] GenreRequest genreRequest)
        {
            try
            {
                await _genreService.Update(id,genreRequest);
                return Ok("GENRE UPDATED WITH ID "+id);
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
                await _genreService.Delete(id);
                return Ok("GENRE DELETED WITH ID "+id);
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
