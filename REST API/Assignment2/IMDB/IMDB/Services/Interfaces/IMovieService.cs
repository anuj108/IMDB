using IMDB.Models;

namespace IMDB.Services.Interfaces
{
    public interface IMovieService
    {
        void Create(Movie movie);

        IList<Movie> GetByYear(int year);
        IList<Movie> Get();
        Movie Get(int id);

        void Update(Movie movie);

        void Delete(int id);
    }
}
