
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IGenreService
    {
        Task<int> Create(GenreRequest genreRequest);
        Task<IEnumerable<GenreResponse>> Get();
        Task<GenreResponse> GetById(int id);

        Task Update(int id,GenreRequest genreRequest);
        Task Delete(int id);
        Task<IEnumerable<GenreResponse>> GetGenresForMovie(int id);
    }
}
