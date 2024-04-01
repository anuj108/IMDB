using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Review Create(Review review);

        IList<Review> Get();
        List<Review> GetByMovieId(int movieId);

        Review Get(int id);

        void Update(Review review);

        void Delete(int id);
    }
}
