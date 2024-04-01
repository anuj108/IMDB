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

        public IList<GenreResponse> Get()
        {
            if(!_genreRepository.Get().Any()) throw new BadRequestException("Empty");
            var responseData=_genreRepository.Get();
            return responseData.Select(x=> new GenreResponse { Id = x.Id, Name = x.Name }).ToList();
        }

        public GenreResponse Get(int id)
        {
            if (id>_genreRepository.Get().Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            var responseData= _genreRepository.Get(id);
            return new GenreResponse
            {
                Id = id,
                Name = responseData.Name,
            };
        }

        public void Update(int id,GenreRequest genreRequest)
        {
            if (id>_genreRepository.Get().Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            _genreRepository.Update(new Genre
            {
                Id= id,
                Name = genreRequest.Name,
            });
        }
        public void Delete(int id)
        {
            if (id>_genreRepository.Get().Last().Id || id<=0) throw new BadRequestException("Invalid Id");
            _genreRepository.Delete(id);
        }
    }
}
