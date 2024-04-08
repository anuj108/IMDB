using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<int> Create(Movie movie);
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Get(int id);

        Task<IEnumerable<Movie>> GetByYear(int year);

        Task Update(Movie movie);

        Task Delete(int id);

    }
}
