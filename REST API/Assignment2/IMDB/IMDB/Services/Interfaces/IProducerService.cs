using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;

namespace IMDB.Services.Interfaces
{
    public interface IProducerService
    {
        IList<ProducerResponse> Get();
        ProducerResponse Get(int id);
        Producer Create(ProducerRequest producerRequest);
        void Update(int id,ProducerRequest producerRequest);
        void Delete(int id);
    }
}
