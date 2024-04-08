using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        Task<IEnumerable<Producer>> Get();
        Task<Producer> Get(int id);
        Task<int> Create(Producer actor);
        Task Update(Producer actor);
        Task Delete(int id);
    }
}
