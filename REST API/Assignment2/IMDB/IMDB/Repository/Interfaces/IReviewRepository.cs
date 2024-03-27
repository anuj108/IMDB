using IMDB.Models;

namespace IMDB.Repository.Interfaces
{
    public interface IReviewRepository
    {
        void Create(Review review);
        List<Review> Get(int id);

        Review GetById(int movieId,int id);

        void Update(Review review);

        void Delete(int id);
    }
}
