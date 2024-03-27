using IMDB.Models;
using IMDB.Repository.Interfaces;

namespace IMDB.Repository
{
    public class ReviewRepository:IReviewRepository
    {
        private readonly List<Review> _reviewRepository;
        public ReviewRepository()
        {
            _reviewRepository = new List<Review>();
        }

        public void Create(Review review)
        {
            _reviewRepository.Add(review);
        }

        public List<Review> Get(int movieId)
        {
            return _reviewRepository.Where(review=>review.MovieId == movieId).ToList();
        }

        public Review GetById(int movieId,int id) {
            return _reviewRepository.FirstOrDefault(review => review.MovieId==movieId&&review.Id==id);
        }

        public void Update(Review review)
        {
            var reviewId=_reviewRepository.FindIndex(CurrentReview=>CurrentReview.Id==review.Id);
            _reviewRepository[reviewId] = review;
        }

        public void Delete(int id)
        {
            var reviewToDelete=_reviewRepository.FirstOrDefault(review=>review.Id==id);
            _reviewRepository.Remove(reviewToDelete);
        }
    }
}
