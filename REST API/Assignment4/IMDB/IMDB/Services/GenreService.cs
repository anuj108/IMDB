using IMDB.CustomExceptions;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using IMDB.Domain.Response;
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
            var genreData = await _genreRepository.Get();
            if (!genreData.Any()) throw new NotFoundException("EMPTY GENRE LIST");
            return genreData.Select(x=> new GenreResponse { Id = x.Id, Name = x.Name }).ToList();
        }

        public async Task<GenreResponse> GetById(int id)
        {
            var genreData = await _genreRepository.GetById(id);
            if (genreData == null) throw new NotFoundException("EMPTY GENRE LIST");
            return new GenreResponse
            {
                Id = genreData.Id,
                Name = genreData.Name
            };
        }

        public async Task Update(int id,GenreRequest genreRequest)
        {
            if (await _genreRepository.GetById(id)==null) throw new NotFoundException("NO GENRE FOUND");
            if (string.IsNullOrWhiteSpace(genreRequest.Name)) throw new BadRequestException("INVALID GENRE NAME");
            await _genreRepository.Update(new Genre
            {
                Id= id,
                Name = genreRequest.Name,
            });
        }
        public async Task Delete(int id)
        {
            if (await _genreRepository.GetById(id)==null) throw new NotFoundException("NO GENRE FOUND");
            await _genreRepository.Delete(id);
        }

        //TO GET STRING OF IDS OF GENRES USING genreS_MOVIES TABLE
        public async Task<IEnumerable<GenreResponse>> GetGenresForMovie(int id)
        {
            var genreData = await _genreRepository.GetGenresForMovie(id);
           
            if (genreData == null) throw new NotFoundException("EMPTY GENRE LIST");
            return genreData.Select(x => new GenreResponse()
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

        }

    }
}
