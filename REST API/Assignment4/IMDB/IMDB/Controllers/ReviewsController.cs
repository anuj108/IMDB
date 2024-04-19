using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService= reviewService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _reviewService.Get());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //To get all reviews for a movie
        [HttpGet]
        [Route("movie/{movieId}")]
        public async Task<IActionResult> GetByMovieId([FromRoute] int movieId)
        {
            try
            {
                return Ok(await _reviewService.GetByMovieId(movieId));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        //To get a specific review for a movie 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview([FromRoute]int id)
        {
            try
            {
                return Ok(await _reviewService.Get(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody]ReviewRequest reviewRequest)
        {

            try
            {
                var addedReview = await _reviewService.Create(reviewRequest);
                var routeValues = new { id = addedReview };
                return CreatedAtAction("Get", routeValues, addedReview);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,[FromBody]ReviewRequest review)
        {
            try
            {
                await _reviewService.Update(id, review);
                return Ok("REVIEW UPDATED WITH ID: "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            try
            {
                await _reviewService.Delete(id);
                return Ok("REVIEW DELETED WITH ID: "+id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
