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
        public Movie Create(MovieRequest movieRequest)
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
                actors.Add(_actorRepository.Get(movieRequest.ActorIds[i]));
            }
            for (int i = 0; i<movieRequest.GenreIds.Count; i++)
            {
                genres.Add(_genreRepository.Get(movieRequest.GenreIds[i]));
            }
            var producer = _producerRepository.Get(movieRequest.ProducerId);
            _id++;
            return _movieRepository.Create(new Movie
            {
                Id = _id,
                Name = movieRequest.Name,
                YearOfRelease = movieRequest.YearOfRelease,
                Plot = movieRequest.Plot,
                Actors = actors,
                Genres = genres,
                Producer = producer,
                CoverImage = movieRequest.CoverImage
            });
        }

        public List<MovieResponse> Get()
        {
            var movieData = _movieRepository.Get();
            if (movieData==null) throw new BadRequestException("Not valid");
            return movieData.Select(x=>new MovieResponse
            {
                Id=x.Id,
                Name = x.Name,
                YearOfRelease = x.YearOfRelease,
                Plot = x.Plot,
                Actors=x.Actors.Select(y => y.Name).ToList(),
                Genres=x.Genres.Select(y => y.Name).ToList(),
                Producer=x.Producer.Name,
                CoverImage=x.CoverImage
            }).ToList();
        }

        public MovieResponse Get(int id)
        {
            var movie = _movieRepository.Get(id);
            if (movie==null) throw new BadRequestException("Not valid");
            
            return new MovieResponse
            {
                Id=id,
                Name = movie.Name,
                YearOfRelease=movie.YearOfRelease,
                Plot = movie.Plot,
                Actors=x.Actors.Select(y => y.Name).ToList(),
                Genres=x.Genres.Select(y => y.Name).ToList(),
                Producer=movie.Producer.Name,
                CoverImage=movie.CoverImage
            };
        }

        public List<MovieResponse> GetByYear(int year) {
            if (year < 1800 || year > DateTime.Now.Year + 10) throw new BadRequestException("Not valid"); 
            var movieData= _movieRepository.GetByYear(year);
            return movieData.Select(x=> new MovieResponse
            {
                Id = x.Id,
                Name= x.Name,
                YearOfRelease=x.YearOfRelease,
                Plot = x.Plot,
                Actors=x.Actors.Select(y => y.Name).ToList(),
                Genres=x.Genres.Select(y => y.Name).ToList(),
                Producer=x.Producer.Name,
                CoverImage=x.CoverImage
            }).ToList();
        }
        public void Update(int id,MovieRequest movieRequest)
        {
            if (!_movieRepository.Get().Any(x => x.Id==id)) throw new BadRequestException("Not valid");
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
                Actors=movieRequest.ActorIds.Select(x=>_actorRepository.Get(x)).ToList(),
                Genres=movieRequest.GenreIds.Select(x=>_genreRepository.Get(x)).ToList(),
                Producer=_producerRepository.Get(movieRequest.ProducerId),
                CoverImage=movieRequest.CoverImage
            });
        }
        public void Delete(int id) {
            if (_movieRepository.Get(id)==null) throw new BadRequestException("Not valid");
            _movieRepository.Delete(id);
        }
    }
}
