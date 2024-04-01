using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IReviewService
    {
        Review Create(ReviewRequest review);
        IList<ReviewResponse> Get();
        ReviewResponse Get(int id);
        IList<ReviewResponse> GetByMovieId(int movieId);


        void Update(int id,ReviewRequest review);

        void Delete(int id);
    }
}
