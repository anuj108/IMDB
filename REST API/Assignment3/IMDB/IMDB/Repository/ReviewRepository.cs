using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace IMDB.Repository
{
    public class ReviewRepository:BaseRepository<Review>,IReviewRepository
    {
        private readonly ConnectionString _connectionString;
        public ReviewRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString=connectionString.Value;
        }
     
        public async Task<int> Create(Review review)
        {
            var message=review.Message;
            var movieId = review.MovieId;
            const string query = @"Insert into Foundation.reviews([message],[movieId]) select Scope_Identity";
            return await Create(query, new
            {
                Message=message,
                MovieId=movieId,
            });
        }

        //To Get All The Reviews
        public async Task<IEnumerable<Review>> Get()
        {
            const string query = @"Select * from Foundation.reviews";
            return await Get(query);
        }

        //To Get Reviews for a movie
        public async Task<IEnumerable<Review>> GetByMovieId(int movieId)
        {
            const string query = @"Select * from Foundation.reviews ";
            var allReviews=await Get(query);

            return allReviews.Where(x=>x.MovieId==movieId);
            
        }

        //To get a particular review
        public async Task<Review> Get(int id) {
            const string query = @"Select * from Foundation.reviews where [Id]=@id";
            return await Get(query, new { Id = id });
        }

        public async Task Update(Review review)
        {
            var message = review.Message;
            var movieId = review.MovieId;
            const string query = @"Update Foundation.Reviews SET [Message]=@message,[MovieId]=@movieId where [Id]=@id";
            await Update(query, new
            {
                Message=message,
                MovieId=movieId,
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"Delete from FOUNDATION.Reviews where id=@id";
            await Delete(query, new { Id = id });
        }
    }
}
