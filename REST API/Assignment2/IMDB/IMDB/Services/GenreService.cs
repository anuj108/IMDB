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
        private int _id = 0;
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository=genreRepository;
        }
        public Genre Create(GenreRequest genreRequest)
        {
            if (string.IsNullOrWhiteSpace(genreRequest.Name)) throw new BadRequestException("Invalid Name");
            _id++;
            return _genreRepository.Create(
                new Genre
                {
                    Id = _id,
                    Name = genreRequest.Name
                });
        }

        public List<GenreResponse> Get()
        {
            var genreData = _genreRepository.Get();
            if (genreData==null) throw new BadRequestException("Empty");
            
            return genreData.Select(x=> new GenreResponse { Id = x.Id, Name = x.Name }).ToList();
        }

        public GenreResponse Get(int id)
        {
            var genreData = _genreRepository.Get(id);
            if (id>genreData.Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            return new GenreResponse
            {
                Id = id,
                Name = responseData.Name,
            };
        }

        public void Update(int id,GenreRequest genreRequest)
        {
            var genreData = _genreRepository.Get(id);
            if (id>genreData.Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            _genreRepository.Update(new Genre
            {
                Id= id,
                Name = genreRequest.Name,
            });
        }
        public void Delete(int id)
        {
            var genreData = _genreRepository.Get(id);
            if (id>genreData.Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            _genreRepository.Delete(id);
        }
    }
}
