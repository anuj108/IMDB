using IMDB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<int> Create(Review review);

        Task<IEnumerable<Review>> Get();
        Task<IEnumerable<Review>> GetByMovieId(int movieId);

        Task<Review> Get(int id);

        Task Update(Review review);

        Task Delete(int id);
    }
}
