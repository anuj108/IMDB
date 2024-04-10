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

        public async Task<int> Create(ReviewRequest reviewRequest)
        {
            if (string.IsNullOrWhiteSpace(reviewRequest.Message)) throw new BadRequestException("INVALID MESSAGE");
            if (reviewRequest.MovieId>(await _movieRepository.Get()).Last().Id || reviewRequest.MovieId<=0) throw new BadRequestException("INVALID ID");
            
            return await _reviewRepository.Create(new Review
            {
                Message = reviewRequest.Message,
                MovieId = reviewRequest.MovieId
            });   
            
        }

        public async Task<IEnumerable<ReviewResponse>> Get()
        {
            var responseData = await _reviewRepository.Get();
            if (!responseData.Any()) throw new NotFoundException("NO REVIEW FOUND");
            return responseData.Select(x=>new ReviewResponse
            {
                Id =x.Id,
                Message = x.Message,
                MovieId = x.MovieId
            }).ToList();
        }

        public async Task<IEnumerable<ReviewResponse>> GetByMovieId(int movieId) {
            var responseData = await _reviewRepository.GetByMovieId(movieId);
            if (!responseData.Any()) throw new NotFoundException("NO REVIEW FOUND");


            return responseData.Select(x => new ReviewResponse
            {
                Id=x.Id,
                MovieId=x.MovieId,
                Message = x.Message,
            }).ToList();
        }

        public async Task<ReviewResponse> Get(int id)
        {
            var responseData = await _reviewRepository.Get(id);
            if (responseData==null) throw new NotFoundException("NO REVIEW FOUND");

            return new ReviewResponse
            {
                Id=id,
                Message=responseData.Message,
                MovieId=responseData.MovieId
            };
        }

        public async Task Update(int id,ReviewRequest reviewRequest)
        {
            if (string.IsNullOrWhiteSpace(reviewRequest.Message)) throw new BadRequestException("INVALID MESSAGE");
            if (reviewRequest.MovieId> (await _movieRepository.Get()).Last().Id || reviewRequest.MovieId<=0) throw new BadRequestException("INVALID MOVIEID");
            await _reviewRepository.Update(new Review
            {
                Id=id,
                MovieId=reviewRequest.MovieId,
                Message=reviewRequest.Message,
            });
        }

        public async Task Delete(int id)
        {
            var responseData = await _reviewRepository.Get(id);
            if (responseData==null) throw new NotFoundException("NO REVIEW FOUND");
            await _reviewRepository.Delete(id);
        }
    }
}
