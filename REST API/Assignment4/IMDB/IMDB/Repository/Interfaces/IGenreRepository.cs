using IMDB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IGenreRepository
    {
        
        Task<int> Create(Genre genre);
        Task<IEnumerable<Genre>> Get();
        Task<Genre> GetById(int id);

        Task Update(Genre genre);
        Task Delete(int id);
        Task<IEnumerable<Genre>> GetGenresForMovie(int id);
    }
}
