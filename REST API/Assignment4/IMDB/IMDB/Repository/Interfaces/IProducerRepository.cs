using IMDB.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        Task<IEnumerable<Producer>> Get();
        Task<Producer> GetById(int id);
        Task<int> Create(Producer actor);
        Task Update(Producer actor);
        Task Delete(int id);
    }
}
