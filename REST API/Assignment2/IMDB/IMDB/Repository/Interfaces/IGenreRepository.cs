using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        
        Genre Create(Genre genre);
        IList<Genre> Get();
        Genre Get(int id);

        void Update(Genre genre);
        void Delete(int id);
    }
}
