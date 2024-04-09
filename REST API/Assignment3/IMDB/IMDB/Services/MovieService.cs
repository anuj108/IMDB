using IMDB.Repository.Interfaces;
using IMDB.Repository;
using IMDB.Services.Interfaces;
using IMDB.Domain.Model;
using IMDB.Domain.Request;
using System.Security.Cryptography;
using IMDB.Domain.Response;
using System.Xml.Linq;
using IMDB.CustomExceptions;

namespace IMDB.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IGenreRepository _genreRepository;
        private int _id = 0;
        public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, IProducerRepository producerRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository=actorRepository;
            _producerRepository=producerRepository;
            _genreRepository=genreRepository;
        }
        public async Task<int> Create(MovieRequest movieRequest)
        {
            if (string.IsNullOrWhiteSpace(movieRequest.Name)) throw new BadRequestException("Not valid");
            if (movieRequest.YearOfRelease < 1800 || movieRequest.YearOfRelease > DateTime.Now.Year + 10) throw new BadRequestException("Not valid"); 
            if(string.IsNullOrWhiteSpace(movieRequest.Plot)) throw new BadRequestException("Not valid");
            if(movieRequest.ActorIds.Count<=0) throw new BadRequestException("Not valid");
            if (movieRequest.GenreIds.Count<=0) throw new BadRequestException("Not valid");
            if (movieRequest.ProducerId<=0) throw new BadRequestException("Not valid");
            if (string.IsNullOrWhiteSpace(movieRequest.CoverImage)) throw new BadRequestException("Not valid");
            var actors = new List<Actor>();
            var genres = new List<Genre>();
            for (int i=0;i<movieRequest.ActorIds.Count;i++)
            {
                actors.Add(await _actorRepository.Get(movieRequest.ActorIds[i]));
            }
            for (int i = 0; i<movieRequest.GenreIds.Count; i++)
            {
                genres.Add(await _genreRepository.Get(movieRequest.GenreIds[i]));
            }
            var producer =await _producerRepository.Get(movieRequest.ProducerId);
            
            return await _movieRepository.Create(new Movie
            {
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                Actors = actors,
                Genres = genres,
                Producer = producer,
                CoverImage = movieRequest.CoverImage
            });
        }

        public async Task<IEnumerable<MovieResponse>> Get()
        {
            if(!(await _movieRepository.Get()).Any()) throw new BadRequestException("Not valid");
            return (await _movieRepository.Get()).Select(x=>new MovieResponse
            {
                Id=x.Id,
                Name = x.Name,
                YearOfRelease = x.YearOfRelease,
                Plot = x.Plot,
                Actors=string.Join(",",x.Actors.Select(y => y.Name)),
                Genres=string.Join(",", x.Genres.Select(y => y.Name)),
                Producer=x.Producer.Name,
                CoverImage=x.CoverImage
            }).ToList();
        }

        public async Task<MovieResponse> Get(int id)
        {
            if (!(await _movieRepository.Get()).Any(x=>x.Id==id)) throw new BadRequestException("Not valid");
            var responseData= await _movieRepository.Get(id);
            return new MovieResponse
            {
                Id=id,
                Name = responseData.Name,
                YearOfRelease=responseData.YearOfRelease,
                Plot = responseData.Plot,
                Actors=string.Join(",",responseData.Actors.Select(y=>y.Name)),
                Genres=string.Join(",",responseData.Genres.Select(y=>y.Name)),
                Producer=responseData.Producer.Name,
                CoverImage=responseData.CoverImage
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetByYear(int year) {
            if (year < 1800 || year > DateTime.Now.Year + 10) throw new BadRequestException("Not valid"); 
            var responseData= await _movieRepository.GetByYear(year);
            return responseData.Select(x=> new MovieResponse
            {
                Id = x.Id,
                Name= x.Name,
                YearOfRelease=x.YearOfRelease,
                Plot = x.Plot,
                Actors=string.Join(",", x.Actors.Select(y => y.Name)),
                Genres=string.Join(",",x.Genres.Select(y=>y.Name)),
                Producer=x.Producer.Name,
                CoverImage=x.CoverImage
            }).ToList();
        }
        public async Task Update(int id,MovieRequest movieRequest)
        {
            /*if (!_movieRepository.Get().Any(x => x.Id==id)) throw new BadRequestException("Not valid");
            if (string.IsNullOrWhiteSpace(movieRequest.Name)) throw new BadRequestException("Not valid");
            if (movieRequest.YearOfRelease < 1800 || movieRequest.YearOfRelease > DateTime.Now.Year + 10) throw new BadRequestException("Not valid");
            if (string.IsNullOrWhiteSpace(movieRequest.Plot)) throw new BadRequestException("Not valid");
            if (movieRequest.ActorIds.Count<=0) throw new BadRequestException("Not valid");
            if (movieRequest.GenreIds.Count<=0) throw new BadRequestException("Not valid");
            if (movieRequest.ProducerId<=0) throw new BadRequestException("Not valid");
            if (string.IsNullOrWhiteSpace(movieRequest.CoverImage)) throw new BadRequestException("Not valid");
            _movieRepository.Update(new Movie
            {
                Id = id,
                YearOfRelease= movieRequest.YearOfRelease,
                Plot= movieRequest.Plot,
               // Actors=movieRequest.ActorIds.Select(x=>_actorRepository.Get(x)).ToList(),
                Genres=movieRequest.GenreIds.Select(x=>_genreRepository.Get(x)).ToList(),
                Producer=_producerRepository.Get(movieRequest.ProducerId),
                CoverImage=movieRequest.CoverImage
            });
            */
        }
        public async Task Delete(int id) {
            if (!(await _movieRepository.Get()).Any(x => x.Id==id)) throw new BadRequestException("Not valid");
            _movieRepository.Delete(id);
        }
    }
}
