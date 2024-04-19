using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IReviewService
    {
        Task<int> Create(ReviewRequest review);
        Task<IEnumerable<ReviewResponse>> Get();
        Task<ReviewResponse> Get(int id);
        Task<IEnumerable<ReviewResponse>> GetByMovieId(int movieId);


        Task Update(int id,ReviewRequest review);

        Task Delete(int id);
    }
}
