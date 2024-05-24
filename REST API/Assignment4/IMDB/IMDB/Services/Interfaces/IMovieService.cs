
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IMovieService
    {
        Task<int> Create(MovieRequest movie);

        Task<IEnumerable<MovieResponse>> GetByYear(int year);
        Task<IEnumerable<MovieResponse>> Get();
        Task<MovieResponse> GetById(int id);

        Task Update(int id,MovieRequest movie);

        Task Delete(int id);
    }
}
