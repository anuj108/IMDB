using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IGenreService
    {
        Genre Create(GenreRequest genreRequest);
        IList<GenreResponse> Get();
        GenreResponse Get(int id);

        void Update(int id,GenreRequest genreRequest);
        void Delete(int id);
    }
}
