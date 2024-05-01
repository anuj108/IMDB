using IMDB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IMovieRepository
    {
        Task<int> Create(Movie movie,string actorIds,string genreIds);
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Get(int id);

        Task<IEnumerable<Movie>> GetByYear(int year);

        Task Update(Movie movie,string actorIds, string genreIds);

        Task Delete(int id);

    }
}
