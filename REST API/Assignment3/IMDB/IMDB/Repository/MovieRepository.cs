using IMDB.Domain.Model;
using IMDB.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace IMDB.Repository
{
    public class MovieRepository:BaseRepository<Movie>,IMovieRepository
    {
        private readonly ConnectionString _connectionString;
        public MovieRepository(IOptions<ConnectionString> connectionString)
            :base(connectionString.Value.IMDBDB)
        {
            _connectionString=connectionString.Value;
        }
       

        public async Task<int> Create(Movie movie)
        {
            const string query=@"Insert into foundation.movies([Name],[YearOfRelease],[Plot],)"
        }

        public async Task<IEnumerable<Movie>> Get() {

        }

        public async Task<Movie> Get(int id)
        {
            return _movieRepository.FirstOrDefault(movie=>movie.Id==id);
        }

        public async Task<IEnumerable<Movie>> GetByYear(int year)
        {
            return _movieRepository.Where(movie => movie.YearOfRelease==year).ToList();
        }

        public Task Update(Movie movie)
        {
            var movieId = _movieRepository.FindIndex(movie=>movie.Id==movie.Id);
            _movieRepository[movieId] = movie;
        }

        public Task Delete(int id)
        {
            var movieToDelete= _movieRepository.FirstOrDefault(movie=>movie.Id==id);
            _movieRepository.Remove(movieToDelete);
        }
    }
}
