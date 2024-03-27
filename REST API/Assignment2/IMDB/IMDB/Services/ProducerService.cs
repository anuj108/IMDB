using IMDB.Models;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        public ProducerService()
        { 
        _producerRepository =new ProducerRepository();
        }

        public void Create(Producer producer)
        {
            _producerRepository.Create(producer);
        }

        public IList<Producer> Get()
        {
            return _producerRepository.Get();
        }

        public Producer Get(int id)
        {
            return _producerRepository.Get(id);
        }

        public void Update(Producer producer)
        {
            _producerRepository.Update(producer);
        }
        public void Delete(int id)
        {
            _producerRepository.Delete(id);
        }

    }
}
