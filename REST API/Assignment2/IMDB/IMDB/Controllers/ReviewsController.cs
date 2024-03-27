using IMDB.Models;
using IMDB.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMDB.Controllers
{
    [Route("api/movies/{movieId}/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService= reviewService;
        }
        //To get all reviews for a movie
        [HttpGet]
        public IActionResult GetReviews([FromRoute] int movieId)
        {
            return Ok(_reviewService.Get(movieId));
        }
        //To get a specific review for a movie 
        [HttpGet("{id}")]
        public IActionResult GetReview([FromRoute]int movieId,[FromRoute]int id)
        {
            return Ok(_reviewService.GetById(movieId,id));
        }

        [HttpPost]
        public IActionResult AddReview([FromBody]Review review, [FromRoute] int movieId)
        {
            _reviewService.Create(review);
            return Ok(_reviewService.Get(movieId));
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody]Review review, [FromRoute] int movieId)
        {
            _reviewService.Update(review);
            return Ok(_reviewService.Get(movieId));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id, [FromRoute]int movieId)
        {
            _reviewService.Delete(id);
            return Ok(_reviewService.Get(movieId));
        }
    }
}
