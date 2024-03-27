using IMDB.Models;

namespace IMDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        
        void Create(Genre genre);
        IList<Genre> Get();
        Genre Get(int id);

        void Update(Genre genre);
        void Delete(int id);
    }
}
