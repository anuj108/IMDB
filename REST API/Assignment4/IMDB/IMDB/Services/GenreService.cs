using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
using IMDB.Repository;
using IMDB.Repository.Interfaces;
using IMDB.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (string.IsNullOrWhiteSpace(genreRequest.Name)) throw new BadRequestException("INVALID GENRE NAME");
            
            return await _genreRepository.Create(
                new Genre
                {
                    Name = genreRequest.Name
                });
        }

        public async Task<IEnumerable<GenreResponse>> Get()
        {
            var responseData = await _genreRepository.Get();
            if (!responseData.Any()) throw new NotFoundException("EMPTY GENRE LIST");
            return responseData.Select(x=> new GenreResponse { Id = x.Id, Name = x.Name }).ToList();
        }

        public async Task<GenreResponse> Get(int id)
        {
            var responseData = await _genreRepository.Get(id);
            if (responseData == null) throw new NotFoundException("EMPTY GENRE LIST");
            return new GenreResponse
            {
                Id = responseData.Id,
                Name = responseData.Name
            };
        }

        public async Task Update(int id,GenreRequest genreRequest)
        {
            if (await _genreRepository.Get(id)==null) throw new NotFoundException("NO GENRE FOUND");
            if (string.IsNullOrWhiteSpace(genreRequest.Name)) throw new BadRequestException("INVALID GENRE NAME");
            await _genreRepository.Update(new Genre
            {
                Id= id,
                Name = genreRequest.Name,
            });
        }
        public async Task Delete(int id)
        {
            if (await _genreRepository.Get(id)==null) throw new NotFoundException("NO GENRE FOUND");
            await _genreRepository.Delete(id);
        }
    }
}
