using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Movie Create(Movie movie);
        IList<Movie> Get();
        Movie Get(int id);

        IList<Movie> GetByYear(int year);

        void Update(Movie movie);

        void Delete(int id);

    }
}
