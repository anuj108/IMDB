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
        
        public ProducerService(IProducerRepository producerRepository)
        { 
        _producerRepository =producerRepository;
        }

        public async Task<int> Create(ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            
            return await _producerRepository.Create(new Producer
            {
               
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
            });
        }

        public async Task<IEnumerable<ProducerResponse>> Get()
        {
            var responseData=await _producerRepository.Get();
            if (!responseData.Any()) throw new BadRequestException("Invalid Request");
            return responseData.Select(x=>new ProducerResponse { Id=x.Id, Name=x.Name, Bio=x.Bio,Gender=x.Gender}).ToList();
        }

        public async Task<ProducerResponse> Get(int id)
        {
            var responseData = await _producerRepository.Get(id);
            if (responseData == null) throw new BadRequestException("No Producer Found");
            return new ProducerResponse
            {
                Id = responseData.Id,
                Name=responseData.Name,
                Bio=responseData.Bio,
                DOB=responseData.DOB,
                Gender=responseData.Gender
            };
        }

        public async Task Update(int id,ProducerRequest producerRequest)
        {
            if (string.IsNullOrWhiteSpace(producerRequest.Name)) throw new BadRequestException("Invalid Name");
            if (string.IsNullOrWhiteSpace(producerRequest.Bio)) throw new BadRequestException("Invalid Bio Data");
            if (producerRequest.DOB.Year < 1800 || producerRequest.DOB.Year > DateTime.Now.Year) throw new BadRequestException("Invalid Date of Birth");
            if (!producerRequest.Gender.Equals("Male") && !producerRequest.Gender.Equals("Female")) throw new BadRequestException("Invalid Gender");
            await _producerRepository.Update(
                new Producer { 
                    Id=id,
                Name=producerRequest.Name,
                Bio=producerRequest.Bio,
                DOB=producerRequest.DOB,
                Gender=producerRequest.Gender,
                });
        }
        public async Task Delete(int id)
        {
            if (_producerRepository.Get(id)==null) throw new BadRequestException("Invalid Id");
            await _producerRepository.Delete(id);
        }

    }
}
