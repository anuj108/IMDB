using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class ProducerService:IProducerService
    {
        private readonly IProducerRepository _producerRepository;
        private int _id = 0;
        public ProducerService(IProducerRepository producerRepository)
        { 
        _producerRepository =producerRepository;
        }

        public Producer Create(ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            _id++;
            return _producerRepository.Create(new Producer
            {
                Id=_id,
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
            });
        }

        public IList<ProducerResponse> Get()
        {
            var responseData= _producerRepository.Get();
            if (responseData.Any()) throw new BadRequestException("Invalid Request");
            return responseData.Select(x=>new ProducerResponse { Id=x.Id, Name=x.Name, Bio=x.Bio,Gender=x.Gender}).ToList();
        }

        public ProducerResponse Get(int id)
        {
            if(!_producerRepository.Get().Any(x=>x.Id==id)) throw new BadRequestException("Invalid Request");
            var responseData= _producerRepository.Get(id);
            return new ProducerResponse
            {
                Id=responseData.Id,
                Name=responseData.Name,
                Bio=responseData.Bio,
                DOB=responseData.DOB,
                Gender=responseData.Gender
            };
        }

        public void Update(int id,ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            _producerRepository.Update(
                new Producer { Id=id,
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
                });
        }
        public void Delete(int id)
        {
            if (id>_producerRepository.Get().Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            _producerRepository.Delete(id);
        }

    }
}
