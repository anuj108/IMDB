using Firebase.Storage;
using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        public async Task<IActionResult> GetAllMovies()
        {
            try
            {
                return Ok(await _movieService.Get());
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

        [HttpGet("filter")]
  
        public async Task<IActionResult> GetAllMoviesByYear([FromQuery] int year)
        {
            try
            {
                return Ok(await _movieService.GetByYear(year));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }
        //To get an Movie by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            try
            {
                return Ok(await _movieService.Get(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddMovie(MovieRequest movieRequest)
        {
            try
            {
                var addedMovie=await _movieService.Create(movieRequest);
                var routeValues=new {id=addedMovie};
                return CreatedAtAction("GetMovie",routeValues,addedMovie);
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("file not selected");
            var task = await new FirebaseStorage("imdb-3fcd7.appspot.com")
                    .Child("CoverImages")
                    .Child(Guid.NewGuid().ToString() + ".png")
                    .PutAsync(file.OpenReadStream());
            return Ok(task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody] MovieRequest movieRequest)
        {
            try
            {
               await _movieService.Update(id, movieRequest);
                return Ok("MOVIE UPDATED WITH ID : "+id);
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
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _movieService.Delete(id);
                return Ok("MOVIE DELETED WITH ID :"+id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
