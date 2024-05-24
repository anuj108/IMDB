using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            const string query = @"INSERT INTO Foundation.reviews (
	[Message]
	,[MovieId]
	)
values(@Message,@MovieId)

SELECT SCOPE_IDENTITY()";
            return await Create(query, new
            {
                Message=message,
                MovieId=movieId,
            });
        }

        //To Get All The Reviews
        public async Task<IEnumerable<Review>> Get()
        {
            const string query = @"SELECT [Id]
	,[Message]
	,[MovieId]
FROM Foundation.reviews";
            return await Get(query);
        }

        //To Get Reviews for a movie
        public async Task<IEnumerable<Review>> GetByMovieId(int movieId)
        {
            string query = $@"
            SELECT [Id]
            ,[Message]
            ,[MovieId]
            FROM Foundation.reviews
            WHERE MovieId = {movieId}
            ";


            return await Get(query);

            
        }

        //To get a particular review
        public async Task<Review> GetById(int id) {
            const string query = @"SELECT [Id]
	,[Message]
	,[MovieId]
FROM Foundation.reviews
WHERE [Id] = @id";
            return await GetById(query, new { Id = id });
        }

        public async Task Update(Review review)
        {
            var message = review.Message;
            var movieId = review.MovieId;
            var id = review.Id;
            const string query = @"UPDATE Foundation.Reviews
SET [Message] = @Message
	,[MovieId] = @MovieId
WHERE [Id] = @Id";
            await Update(query, new
            {
                Message=message,
                MovieId=movieId,
                Id=id
            });
        }

        public async Task Delete(int id)
        {
            const string query = @"DELETE
FROM FOUNDATION.Reviews
WHERE [Id] = @id";
            await Delete(query, new { Id = id });
        }
    }
}
