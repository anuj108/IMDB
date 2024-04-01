using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IMovieService
    {
        Movie Create(MovieRequest movie);

        IList<MovieResponse> GetByYear(int year);
        IList<MovieResponse> Get();
        MovieResponse Get(int id);

        void Update(int id,MovieRequest movie);

        void Delete(int id);
    }
}
