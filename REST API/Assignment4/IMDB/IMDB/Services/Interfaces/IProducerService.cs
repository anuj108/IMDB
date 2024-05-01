using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IMDB.Services.Interfaces
{
    public interface IProducerService
    {
        Task<IEnumerable<ProducerResponse>> Get();
        Task<ProducerResponse> Get(int id);
        Task<int> Create(ProducerRequest producerRequest);
        Task Update(int id,ProducerRequest producerRequest);
        Task Delete(int id);
      
    }
}
