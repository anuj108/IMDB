using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            try
            {
                return Ok(_reviewService.Get());
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //To get all reviews for a movie
        [HttpGet]
        [Route("movie/{movieId}")]
        public IActionResult GetByMovieId([FromRoute] int movieId)
        {
            try
            {
                return Ok(_reviewService.GetByMovieId(movieId));
            }
            catch(BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        //To get a specific review for a movie 
        [HttpGet("{id}")]
        public IActionResult GetReview([FromRoute]int id)
        {
            try
            {
                return Ok(_reviewService.Get(id));
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddReview([FromBody]ReviewRequest reviewRequest)
        {

            try
            {
                var addedReview = _reviewService.Create(reviewRequest);
                var routeValues = new { id = addedReview.Id };
                return CreatedAtAction("Get", routeValues, addedReview);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id,[FromBody]ReviewRequest review)
        {
            try
            {
                _reviewService.Update(id, review);
                return Ok("Review updated with id "+id);
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
                _reviewService.Delete(id);
                return Ok("Review deleted with id "+id);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
