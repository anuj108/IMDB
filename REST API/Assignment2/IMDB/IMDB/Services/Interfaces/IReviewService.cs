using IMDB.Models;

namespace IMDB.Services.Interfaces
{
    public interface IReviewService
    {
        void Create(Review review);
        List<Review> Get(int id);

        Review GetById(int movieId, int id);
        void Update(Review review);

        void Delete(int id);
    }
}
