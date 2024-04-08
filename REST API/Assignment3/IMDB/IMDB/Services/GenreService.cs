using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class GenreService:IGenreService
    {
        private readonly IGenreRepository _genreRepository;
    
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository=genreRepository;
        }
        public async Task<int> Create(GenreRequest genreRequest)
        {
            if (string.IsNullOrWhiteSpace(genreRequest.Name)) throw new BadRequestException("Invalid Name");
            
            return await _genreRepository.Create(
                new Genre
                {
                    Name = genreRequest.Name
                });
        }

        public async Task<IEnumerable<GenreResponse>> Get()
        {
            var responseData = await _genreRepository.Get();
            if (!responseData.Any()) throw new BadRequestException("Empty Genre List returned");
            return responseData.Select(x=> new GenreResponse { Id = x.Id, Name = x.Name }).ToList();
        }

        public async Task<GenreResponse> Get(int id)
        {
            var responseData = await _genreRepository.Get(id);
            if (responseData == null) throw new BadRequestException("No Genre Found");
            return new GenreResponse
            {
                Id = responseData.Id,
                Name = responseData.Name
            };
        }

        public async Task Update(int id,GenreRequest genreRequest)
        {
            if (await _genreRepository.Get(id)==null) throw new BadRequestException("Invalid Id");
            await _genreRepository.Update(new Genre
            {
                Id= id,
                Name = genreRequest.Name,
            });
        }
        public async Task Delete(int id)
        {
            if (await _genreRepository.Get(id)==null) throw new BadRequestException("Invalid Id");
            await _genreRepository.Delete(id);
        }
    }
}
