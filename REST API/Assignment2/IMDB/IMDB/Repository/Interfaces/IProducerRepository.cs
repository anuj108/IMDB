using IMDB.Models;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        IList<Producer> Get();
        Producer Get(int id);
        void Create(Producer actor);
        void Update(Producer actor);
        void Delete(int id);
    }
}
