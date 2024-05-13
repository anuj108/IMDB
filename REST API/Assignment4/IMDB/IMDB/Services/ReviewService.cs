using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMDB.Services
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMovieRepository _movieRepository;
  
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
            var reviewData = await _reviewRepository.Get();
            if (reviewData==null) throw new NotFoundException("NO REVIEW FOUND");
            return reviewData.Select(x=>new ReviewResponse
            {
                Id =x.Id,
                Message = x.Message,
                MovieName = _movieRepository.Get(x.MovieId).Result.Name
            }).ToList();
        }

        public async Task<IEnumerable<ReviewResponse>> GetByMovieId(int movieId) {
            var reviewData = await _reviewRepository.GetByMovieId(movieId);
            if (!reviewData.Any()) throw new NotFoundException("NO REVIEW FOUND");


            return reviewData.Select(x => new ReviewResponse
            {
                Id=x.Id,
                MovieName=_movieRepository.Get(x.MovieId).Result.Name,
                Message = x.Message,
            }).ToList();
        }

        public async Task<ReviewResponse> Get(int id)
        {
            var reviewData = await _reviewRepository.Get(id);
            if (reviewData==null) throw new NotFoundException("NO REVIEW FOUND");

            return new ReviewResponse
            {
                Id=id,
                Message=reviewData.Message,
                MovieName=_movieRepository.Get(reviewData.MovieId).Result.Name,
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
            var reviewData = await _reviewRepository.Get(id);
            if (reviewData==null) throw new NotFoundException("NO REVIEW FOUND");
            await _reviewRepository.Delete(id);
        }
    }
}
