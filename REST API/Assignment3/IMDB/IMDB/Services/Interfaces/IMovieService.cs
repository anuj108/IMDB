using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IMovieService
    {
        Task<int> Create(MovieRequest movie);

        Task<IEnumerable<MovieResponse>> GetByYear(int year);
        Task<IEnumerable<MovieResponse>> Get();
        Task<MovieResponse> Get(int id);

        Task Update(int id,MovieRequest movie);

        Task Delete(int id);
    }
}
