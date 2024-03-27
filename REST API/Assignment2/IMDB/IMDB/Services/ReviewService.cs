using IMDB.Models;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService() { 
            _reviewRepository=new ReviewRepository();
        }

        public void Create(Review review)
        {
            _reviewRepository.Create(review);   
        }

        public List<Review> Get(int movieId) {
            return _reviewRepository.Get(movieId);
        }

        public Review GetById(int movieId,int id)
        {
            return _reviewRepository.GetById(movieId, id);
        }

        public void Update(Review review)
        {
            _reviewRepository.Update(review);
        }

        public void Delete(int id)
        {
            _reviewRepository.Delete(id);
        }
    }
}
