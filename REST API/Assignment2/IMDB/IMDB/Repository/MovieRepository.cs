using IMDB.Models;
using IMDB.Repository.Interfaces;

namespace IMDB.Repository
{
    public class MovieRepository:IMovieRepository
    {
        private readonly List<Movie> _movieRepository;
        public MovieRepository() { 
        _movieRepository =new List<Movie>();
        }

        public void Create(Movie movie)
        {
            _movieRepository.Add(movie);
        }

        public IList<Movie> Get() {

            return _movieRepository;
        }

        public Movie Get(int id)
        {
            return _movieRepository.FirstOrDefault(movie=>movie.Id==id);
        }

        public IList<Movie> GetByYear(int year)
        {
            return _movieRepository.Where(movie => movie.YearOfRelease==year).ToList();
        }

        public void Update(Movie movie)
        {
            var movieId = _movieRepository.FindIndex(movie=>movie.Id==movie.Id);
            _movieRepository[movieId] = movie;
        }

        public void Delete(int id)
        {
            var movieToDelete= _movieRepository.FirstOrDefault(movie=>movie.Id==id);
            _movieRepository.Remove(movieToDelete);
        }
    }
}
