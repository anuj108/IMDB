using IMDB.Models;
using IMDB.Repository.Interfaces;
using IMDB.Repository;
using IMDB.Services.Interfaces;

namespace IMDB.Services
{
    public class MovieService:IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorService _actorService;
        private readonly IProducerService _producerService;
        private readonly IGenreService _genreService;
        public MovieService()
        {
            _movieRepository = new MovieRepository();
            _actorService=new ActorService();
            _producerService=new ProducerService();
            _genreService=new GenreService();
        }
        public void Create(Movie movie)
        {
            _movieRepository.Create(movie);
        }

        public IList<Movie> Get()
        {
            return _movieRepository.Get();
        }

        public Movie Get(int id)
        {
            return _movieRepository.Get(id);
        }

        public IList<Movie> GetByYear(int year) { 
        return _movieRepository.GetByYear(year);
        }
        public void Update(Movie movie)
        {
            _movieRepository.Update(movie);
        }
        public void Delete(int id) { 
            _movieRepository.Delete(id);
        }
    }
}
