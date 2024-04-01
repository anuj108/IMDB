using IMDB.Domain.Model;
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

        public Review Create(Review review)
        {
            _reviewRepository.Add(review);
            return review;
        }

        //To Get All The Reviews
        public IList<Review> Get()
        {
            return _reviewRepository;
        }

        //To Get Reviews for a movie
        public List<Review> GetByMovieId(int movieId)
        {
            return _reviewRepository.Where(review=>review.MovieId == movieId).ToList();
        }

        //To get a particular review
        public Review Get(int id) {
            return _reviewRepository.FirstOrDefault(review =>review.Id==id);
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
