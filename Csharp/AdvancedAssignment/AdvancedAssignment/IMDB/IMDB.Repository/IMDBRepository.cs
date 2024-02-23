using IMDB.Domain;
namespace IMDB.Repository
{
    public class IMDBRepository:IIMDBRepository
    {
        private readonly List<Movie> _movie;
        public IMDBRepository()
        {
            _movie = new List<Movie>();
        }

        public void Add(Movie movie)
        {
            _movie.Add(movie);
        }

        public List<Movie> List()
        {
            return _movie.ToList();
        }
    }
}
