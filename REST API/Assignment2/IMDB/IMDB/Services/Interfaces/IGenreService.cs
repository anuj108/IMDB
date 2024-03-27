using IMDB.Models;

namespace IMDB.Services.Interfaces
{
    public interface IGenreService
    {
        void Create(Genre genre);
        IList<Genre> Get();
        Genre Get(int id);

        void Update(Genre genre);
        void Delete(int id);
    }
}
