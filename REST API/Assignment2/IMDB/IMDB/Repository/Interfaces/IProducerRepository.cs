using IMDB.Domain.Model;

namespace IMDB.Repository.Interfaces
{
    public interface IProducerRepository
    {
        IList<Producer> Get();
        Producer Get(int id);
        Producer Create(Producer actor);
        void Update(Producer actor);
        void Delete(int id);
    }
}
