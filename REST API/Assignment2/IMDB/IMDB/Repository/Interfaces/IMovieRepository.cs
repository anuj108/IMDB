using IMDB.Models;

namespace IMDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        void Create(Movie movie);
        IList<Movie> Get();
        Movie Get(int id);

        IList<Movie> GetByYear(int year);

        void Update(Movie movie);

        void Delete(int id);

    }
}
