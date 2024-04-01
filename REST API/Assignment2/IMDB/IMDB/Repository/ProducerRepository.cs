
using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;

namespace IMDB.Repository
{
    public class ProducerRepository:IProducerRepository
    {
        private readonly List<Producer> _producerRepository;
        public ProducerRepository() {
            _producerRepository=new List<Producer>();
        }

        public Producer Create(Producer producer)
        {
            _producerRepository.Add(producer);
            return producer;
        }

        public IList<Producer> Get()
        {
            return _producerRepository;
        }

        public Producer Get(int id)
        {
            return _producerRepository.FirstOrDefault(producer=>producer.Id==id);
        }

        public void Update(Producer producer)
        {
            var producerId = _producerRepository.FindIndex(cProducer=>cProducer.Id==producer.Id);
            _producerRepository[producerId] = producer;
        }

        public void Delete(int id)
        {
            var producerToDelete = Get(id);
            _producerRepository.Remove(producerToDelete);
        }
    }
}
