using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        
        Task<int> Create(Genre genre);
        Task<IEnumerable<Genre>> Get();
        Task<Genre> Get(int id);

        Task Update(Genre genre);
        Task Delete(int id);
    }
}
