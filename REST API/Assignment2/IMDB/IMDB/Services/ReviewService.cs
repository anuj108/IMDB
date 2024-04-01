using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
        private int _id = 0;
        public ReviewService(IReviewRepository reviewRepository,IMovieRepository movieRepository) {
            _reviewRepository = reviewRepository;
            _movieRepository = movieRepository;
        }

        public Review Create(ReviewRequest reviewRequest)
        {
            if (string.IsNullOrWhiteSpace(reviewRequest.Message)) throw new BadRequestException("Invalid Message");
            if (reviewRequest.MovieId>_movieRepository.Get().Last().Id || reviewRequest.MovieId<=0) throw new BadRequestException("Invalid Id");
            _id++;
            return _reviewRepository.Create(new Review
            {
                Id = _id,
                Message = reviewRequest.Message,
                MovieId = reviewRequest.MovieId
            });   
            
        }

        public IList<ReviewResponse> Get()
        {
            if (!_reviewRepository.Get().Any()) throw new BadRequestException("EMPTY");
            return _reviewRepository.Get().Select(x=>new ReviewResponse
            {
                Id =x.Id,
                Message = x.Message,
                MovieId = x.MovieId
            }).ToList();
        }

        public IList<ReviewResponse> GetByMovieId(int movieId) {
            if(!_reviewRepository.GetByMovieId(movieId).Any()) throw new BadRequestException("Invalid Message");
            if (movieId>_movieRepository.Get().Last().Id ||  movieId<=0) throw new BadRequestException("Invalid Id");
            var responseData=_reviewRepository.GetByMovieId(movieId);
            return responseData.Select(x => new ReviewResponse
            {
                Id=x.Id,
                MovieId=x.MovieId,
                Message = x.Message,
            }).ToList();
        }

        public ReviewResponse Get(int id)
        {
            if (!_reviewRepository.Get().Any(x => x.Id == id)) throw new BadRequestException("Review not Found");
            var responseData= _reviewRepository.Get(id);
            return new ReviewResponse
            {
                Id=id,
                Message=responseData.Message,
                MovieId=responseData.MovieId
            };
        }

        public void Update(int id,ReviewRequest reviewRequest)
        {
            if (string.IsNullOrWhiteSpace(reviewRequest.Message)) throw new BadRequestException("Invalid Message");
            if (reviewRequest.MovieId>_movieRepository.Get().Last().Id || reviewRequest.MovieId<=0) throw new BadRequestException("Invalid Id");
            _reviewRepository.Update(new Review
            {
                Id=id,
                MovieId=reviewRequest.MovieId,
                Message=reviewRequest.Message,
            });
        }

        public void Delete(int id)
        {
            if (id > _reviewRepository.Get().Last().Id || id <= 0) throw new BadRequestException("Invalid Id");
            if (!_reviewRepository.Get().Any(x => x.Id == id)) throw new BadRequestException("Review not Found");
            _reviewRepository.Delete(id);
        }
    }
}
