using IMDB.Models;

namespace IMDB.Services.Interfaces
{
    public interface IProducerService
    {
        IList<Producer> Get();
        Producer Get(int id);
        void Create(Producer  actor);
        void Update(Producer actor);
        void Delete(int id);
    }
}
